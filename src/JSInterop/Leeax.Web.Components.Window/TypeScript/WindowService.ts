const _listeners: { [eventName: string]: (e: any) => void } = {};

/**
 * Scrolls to the given position in the document.
 *
 * @param top The distance to the top in pixel.
 * @param left The distance to the left in pixel.
 * @param smooth Determines whether the scrolling should be smooth
 *
 */
export function scrollTo(top: number, left: number, smooth: boolean): void {

    if (top === null && left === null) {
        return;
    }

    const options: ScrollToOptions = {};

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

/**
 * Gets the value of the given property name.
 *
 * @param propertyName The property name for which the value should be returned.
 *
 */
export function getProperty(propertyName: string): any {
    return window[propertyName];
}

/**
 * Adds the event handler/listener for the given event name.
 *
 * @param event The event name for which the event handler/listener should be removed.
 * @param dotNetMethodName The name of the .NET method to call when the event is raised.
 * @param dotNetInstance The .NET instance on which the callback should be invoked.
 *
 */
export function addEventHandler(event: string, dotNetMethodName: string, dotNetInstance: any): void {

    // Check whether a listener for this event is already defined
    // and remove it if that's the case
    if (event in _listeners) {
        window.removeEventListener(event, _listeners[event]);
    }

    // Define the new callback
    _listeners[event] = (e) => dotNetInstance.invokeMethod(dotNetMethodName, event, stringifyEventArgsAsJson(event, e));

    // Register the listener for the given event
    window.addEventListener(event, _listeners[event]);
}

/**
 * Removes the event handler/listener for the given event name.
 * 
 * @param event The event name for which the event handler/listener should be removed.
 * 
 */
export function removeEventHandler(event: string): void {

    // Check whether a listener for this event is defined
    // and remove it if that's the case
    if (event in _listeners) {
        window.removeEventListener(event, _listeners[event]);

        // Remove callback from object
        delete _listeners[event];
    }
}

/**
 * Helper function which brings the passed arguments in the correct form and returns them as string.
 * 
 * @param event The event name.
 * @param e The arguments to format.
 * 
 */
export function stringifyEventArgsAsJson(event: string, e: any): string {

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

/**
 * Helper function which brings the passed touch points in the correct form.
 *
 * @param collection The collection of touch points. Can be null or undefined.
 *
 */
function getTouchPointCollection(collection: any[]): any[] {
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