const _transitions = {};
function findId(element) {
    for (let i = 0; i < element.attributes.length; i++) {
        if (element.attributes[i].name.startsWith("_bl_")) {
            return element.attributes[i].name;
        }
    }
    return null;
}
function invokeHook(hooks, name, element) {
    if (hooks != null && name in hooks) {
        hooks[name](element);
    }
}
export function registerCallback(instance, element, hooks, action, prefix, duration) {
    if (!element || (action !== "enter" && action !== "leave"))
        return;
    const id = findId(element);
    if (!id)
        return;
    if (id in _transitions) {
        _transitions[id].cancel();
    }
    if (duration > 0) {
        registerCallbackAfterDuration(id, instance, element, hooks, action, prefix, duration);
    }
    else {
        registerCallbackAfterEvent(id, instance, element, hooks, action, prefix);
    }
    prefix = prefix + "-" + action;
    // Request animation-frame(s) to add and remove transition classes
    requestAnimationFrame(() => {
        invokeHook(hooks, "before" + action[0].toUpperCase() + action.substr(1), element);
        element.classList.add(prefix); // 1 frame before "-active"
        requestAnimationFrame(() => {
            element.classList.add(prefix + "-active");
            requestAnimationFrame(() => {
                element.classList.remove(prefix);
                if (action === "leave")
                    element.classList.add(prefix + "-to");
            });
        });
    });
}
function registerCallbackAfterEvent(id, instance, element, hooks, action, prefix) {
    const isTransitionEvent = true;
    const eventName = isTransitionEvent ? "transitionend" : "animationend";
    prefix = prefix + "-" + action;
    // Callback function which gets called when transition finished
    const callback = (args) => {
        if (args.target !== element)
            return;
        instance.invokeMethodAsync('HandleCallback', isTransitionEvent ? args.propertyName : args.animationName);
        if (action === "leave") {
            element.classList.remove(prefix + "-active", prefix + "-to");
            invokeHook(hooks, "afterLeave", element);
        }
        else {
            requestAnimationFrame(() => {
                element.classList.add(prefix + "-to");
                requestAnimationFrame(() => {
                    element.classList.remove(prefix + "-to");
                    element.classList.remove(prefix + "-active");
                    invokeHook(hooks, "afterEnter", element);
                });
            });
        }
        // Remove listener and cancel function
        element.removeEventListener(eventName, callback);
        delete _transitions[id];
    };
    element.addEventListener(eventName, callback);
    // Register cancel function
    _transitions[id] = {
        cancel: function () {
            element.classList.remove(prefix, prefix + "-active", prefix + "-to");
            element.removeEventListener(eventName, callback);
            invokeHook(hooks, action + "Cancelled", element);
        }
    };
}
function registerCallbackAfterDuration(id, instance, element, hooks, action, prefix, duration) {
    prefix = prefix + "-" + action;
    // Callback function which gets called when transition finished
    const callback = () => {
        instance.invokeMethodAsync('HandleCallback', null);
        if (action === "leave") {
            element.classList.remove(prefix + "-active", prefix + "-to");
            invokeHook(hooks, "afterLeave", element);
        }
        else {
            requestAnimationFrame(() => {
                element.classList.add(prefix + "-to");
                requestAnimationFrame(() => {
                    element.classList.remove(prefix + "-to");
                    element.classList.remove(prefix + "-active");
                    invokeHook(hooks, "afterEnter", element);
                });
            });
        }
        // Remove cancel function
        delete _transitions[id];
    };
    const timeoutId = setTimeout(callback, duration);
    // Register cancel function
    _transitions[id] = {
        cancel: function () {
            element.classList.remove(prefix, prefix + "-active", prefix + "-to");
            clearTimeout(timeoutId);
            invokeHook(hooks, action + "Cancelled", element);
        }
    };
}
//# sourceMappingURL=Transition.js.map