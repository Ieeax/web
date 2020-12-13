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
export function registerCallback(instance, element, hooks, action, prefix, type, duration) {
    if (!element
        || (action !== "enter" && action !== "leave")
        || (type !== "animation" && type !== "transition")) {
        console.warn("[Leeax.Web.Components.Presentation.LxTransition] Received invalid values from .NET: Transition couldn't be registered.");
        return;
    }
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
        registerCallbackAfterEvent(id, instance, element, hooks, action, prefix, type);
    }
    prefix = prefix + "-" + action;
    requestAnimationFrame(() => {
        invokeHook(hooks, "before" + action[0].toUpperCase() + action.substr(1), element);
        element.classList.add(prefix, prefix + "-active");
        requestAnimationFrame(() => {
            element.classList.remove(prefix);
            element.classList.add(prefix + "-to");
        });
    });
}
function registerCallbackAfterEvent(id, instance, element, hooks, action, prefix, type) {
    const eventName = type === "animation" ? "animationend" : "transitionend";
    prefix = prefix + "-" + action;
    const callback = (args) => {
        if (args.target !== element)
            return;
        requestAnimationFrame(() => {
            element.classList.remove(prefix + "-active", prefix + "-to");
            invokeHook(hooks, "after" + action[0].toUpperCase() + action.substr(1), element);
            instance.invokeMethodAsync('HandleCallbackAsync', type === "animation" ? args.animationName : args.propertyName);
        });
        element.removeEventListener(eventName, callback);
        delete _transitions[id];
    };
    element.addEventListener(eventName, callback);
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
    const callback = () => {
        requestAnimationFrame(() => {
            element.classList.remove(prefix + "-active", prefix + "-to");
            invokeHook(hooks, "after" + action[0].toUpperCase() + action.substr(1), element);
            instance.invokeMethodAsync('HandleCallbackAsync', null);
        });
        delete _transitions[id];
    };
    const timeoutId = setTimeout(callback, duration);
    _transitions[id] = {
        cancel: function () {
            element.classList.remove(prefix, prefix + "-active", prefix + "-to");
            clearTimeout(timeoutId);
            invokeHook(hooks, action + "Cancelled", element);
        }
    };
}
