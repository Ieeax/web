const _transitions = {};
/**
   * Finds the trough blazor assigned id of the element.
   */
function findId(element) {
    for (let i = 0; i < element.attributes.length; i++) {
        if (element.attributes[i].name.startsWith("_bl_")) {
            return element.attributes[i].name;
        }
    }
    return null;
}
/**
   * Helper function which tries to invoke the specified hook/function for the given element.
   *
   * @param hooks The object in which the functions are defined.
   * @param name The function name to invoke.
   * @param element The element for which the function should be invoked.
   *
   */
function invokeHook(hooks, name, element) {
    if (hooks != null && name in hooks) {
        hooks[name](element);
    }
}
/**
   * Applies transition classes and invokes a callback (back to .NET) when finished.
   *
   * @param instance The .NET instance on which the callback should be invoked.
   * @param element The element on which the transition should be applied.
   * @param hooks A javascript object which can contain hooks/functions for js-callbacks.
   * @param action The action to apply. Either "enter" or "leave".
   * @param prefix The prefix for the transition classes.
   * @param type The type of the transition, determines for which event has to be listened. Either "animation" or "transition".
   * @param duration The duration for this transition action.
   *
   */
export function registerCallback(instance, element, hooks, action, prefix, type, duration) {
    // Validate some values which could be wrong
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
        _transitions[id].cancel(); // Cancel previous transition if not finished yet
    }
    // Register correct transition type based on duration value
    if (duration > 0) {
        registerCallbackAfterDuration(id, instance, element, hooks, action, prefix, duration);
    }
    else {
        registerCallbackAfterEvent(id, instance, element, hooks, action, prefix, type);
    }
    prefix = prefix + "-" + action;
    // Request animation-frame(s) to add and remove transition classes
    requestAnimationFrame(() => {
        invokeHook(hooks, "before" + action[0].toUpperCase() + action.substr(1), element);
        element.classList.add(prefix, prefix + "-active");
        requestAnimationFrame(() => {
            element.classList.remove(prefix);
            element.classList.add(prefix + "-to");
        });
    });
}
/**
   * Applies transition classes and invokes a callback (back to .NET) when finished. The duration will be based on the configured CSS properties.
   *
   * @param instance The .NET instance on which the callback should be invoked.
   * @param element The element on which the transition should be applied.
   * @param hooks A javascript object which can contain hooks/functions for js-callbacks.
   * @param action The action to apply. Either "enter" or "leave".
   * @param prefix The prefix for the transition classes.
   * @param type The type of the transition, determines for which event has to be listened. Either "animation" or "transition".
   *
   */
function registerCallbackAfterEvent(id, instance, element, hooks, action, prefix, type) {
    const eventName = type === "animation" ? "animationend" : "transitionend";
    prefix = prefix + "-" + action;
    // Callback function which gets called when transition finished
    const callback = (args) => {
        if (args.target !== element)
            return;
        requestAnimationFrame(() => {
            element.classList.remove(prefix + "-active", prefix + "-to");
            invokeHook(hooks, "after" + action[0].toUpperCase() + action.substr(1), element);
            // Notify .NET that transition finished
            instance.invokeMethodAsync('HandleCallbackAsync', type === "animation" ? args.animationName : args.propertyName);
        });
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
/**
   * Applies transition classes and invokes a callback (back to .NET) when finished.
   *
   * @param instance The .NET instance on which the callback should be invoked.
   * @param element The element on which the transition should be applied.
   * @param hooks A javascript object which can contain hooks/functions for js-callbacks.
   * @param action The action to apply. Either "enter" or "leave".
   * @param prefix The prefix for the transition classes.
   * @param duration The duration for this transition action.
   *
   */
function registerCallbackAfterDuration(id, instance, element, hooks, action, prefix, duration) {
    prefix = prefix + "-" + action;
    // Callback function which gets called when transition finished
    const callback = () => {
        requestAnimationFrame(() => {
            element.classList.remove(prefix + "-active", prefix + "-to");
            invokeHook(hooks, "after" + action[0].toUpperCase() + action.substr(1), element);
            // Notify .NET that transition finished
            instance.invokeMethodAsync('HandleCallbackAsync', null);
        });
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