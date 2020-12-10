const _windowEvents = {};
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
    if (smooth) {
        options.behavior = "smooth";
    }
    window.scrollTo(options);
}
export function getProperty(propertyName) {
    return window[propertyName];
}
export function addEventHandler(event, sid, dotNetMethod, instance) {
    const that = this;
    _windowEvents[sid] = {
        event: event,
        callback: function callback(e) {
            instance.invokeMethod(dotNetMethod, sid, that.parseEventArgs(event, e));
        }
    };
    window.addEventListener(event, _windowEvents[sid].callback);
}
export function removeEventHandler(sid) {
    if (sid in _windowEvents) {
        const eventData = _windowEvents[sid];
        window.removeEventListener(eventData.event, eventData.callback);
        delete _windowEvents[sid];
    }
}
export function parseEventArgs(event, e) {
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
                Touches: parseTouchPoints(e.touches),
                TargetTouches: parseTouchPoints(e.targetTouches),
                ChangedTouches: parseTouchPoints(e.changedTouches),
                CtrlKey: e.ctrlKey,
                ShiftKey: e.shiftKey,
                AltKey: e.altKey,
                MetaKey: e.metaKey
            });
        case "resize":
            return "{}";
    }
    throw new Error("Event arguments for '" + event + "' cannot be parsed. Event not implemented yet.");
}
function parseTouchPoints(list) {
    const result = [];
    if (!list) {
        return [];
    }
    for (var i = 0; i < list.length; i++) {
        const current = list[i];
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
//# sourceMappingURL=WindowService.js.map