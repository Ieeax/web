const _listeners = {};
export function scrollTo(top, left, smooth) {
    if (top === null && left === null) {
        return;
    }
    const options = {};
    if (top === null) {
        options.left = left;
    }
    else {
        options.top = top;
    }
    if (smooth === true) {
        options.behavior = "smooth";
    }
    window.scrollTo(options);
}
export function getProperty(propertyName) {
    return window[propertyName];
}
export function addEventHandler(event, dotNetMethodName, dotNetInstance) {
    if (event in _listeners) {
        window.removeEventListener(event, _listeners[event]);
    }
    _listeners[event] = (e) => dotNetInstance.invokeMethod(dotNetMethodName, event, stringifyEventArgsAsJson(event, e));
    window.addEventListener(event, _listeners[event]);
}
export function removeEventHandler(event) {
    if (event in _listeners) {
        window.removeEventListener(event, _listeners[event]);
        delete _listeners[event];
    }
}
export function stringifyEventArgsAsJson(event, e) {
    switch (event) {
        case "click":
        case "mouseup":
        case "mousedown":
        case "mousemove":
            return JSON.stringify({
                Detail: 0,
                ScreenX: e.screenX,
                ScreenY: e.screenY,
                ClientX: e.clientX,
                ClientY: e.clientY,
                Button: e.button,
                Buttons: e.buttons,
                CtrlKey: e.ctrlKey,
                ShiftKey: e.shiftKey,
                AltKey: e.altKey,
                MetaKey: e.metaKey
            });
        case "touchstart":
        case "touchend":
        case "touchcancel":
        case "touchmove":
            return JSON.stringify({
                Detail: 0,
                Touches: getTouchPointCollection(e.touches),
                TargetTouches: getTouchPointCollection(e.targetTouches),
                ChangedTouches: getTouchPointCollection(e.changedTouches),
                CtrlKey: e.ctrlKey,
                ShiftKey: e.shiftKey,
                AltKey: e.altKey,
                MetaKey: e.metaKey
            });
        case "resize":
            return "{}";
    }
    throw new Error("Unknown event '" + event + "'. Event arguments couldn't be parsed.");
}
function getTouchPointCollection(collection) {
    const result = [];
    if (!collection) {
        return result;
    }
    for (var i = 0; i < collection.length; i++) {
        const current = collection[i];
        result.push({
            Identifier: 0,
            ScreenX: current.screenX,
            ScreenY: current.screenY,
            ClientX: current.clientX,
            ClientY: current.clientY,
            PageX: current.pageX,
            PageY: current.pageY
        });
    }
    return result;
}
