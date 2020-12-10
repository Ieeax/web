const _clickOutsideOfElementHandlers = {};
export function insertMarkup(parent, value, position, markupType) {
    let target = parent;
    if (typeof parent === "string") {
        target = document.querySelector(parent);
    }
    if (!target) {
        return false;
    }
    if (markupType === "html") {
        target.insertAdjacentHTML(position, value);
    }
    else if (markupType === "text") {
        target.insertAdjacentText(position, value);
    }
    else {
        return false;
    }
    return true;
}
export function removeElement(element) {
    let target = element;
    if (typeof element === "string") {
        target = document.querySelector(element);
    }
    if (!target) {
        return false;
    }
    // Remove the determined target from its parent
    target.parentElement.removeChild(target);
    return true;
}
//export function isInViewport(element: HTMLElement): boolean {
//    if (!(element instanceof HTMLElement)) {
//        return false;
//    }
//    const rect = element.getBoundingClientRect();
//    const html = document.documentElement;
//    return (
//        rect.top >= 0 &&
//        rect.left >= 0 &&
//        rect.bottom <= (window.innerHeight || html.clientHeight) &&
//        rect.right <= (window.innerWidth || html.clientWidth)
//    );
//}
export function scrollIntoView(element, block, inline, smooth) {
    const options = {};
    if (block !== null) {
        options.block = block;
    }
    if (inline !== null) {
        options.inline = inline;
    }
    if (smooth) {
        options.behavior = "smooth";
    }
    element.scrollIntoView(options);
}
export function getBoundingClientRect(element) {
    return element.getBoundingClientRect();
}
export function getPosition(element) {
    const boundingRect = element.getBoundingClientRect();
    const isHidden = boundingRect.left === 0
        && boundingRect.top === 0
        && boundingRect.right === 0
        && boundingRect.bottom === 0;
    return {
        Left: boundingRect.left,
        Top: boundingRect.top,
        Right: isHidden ? 0 : (window.innerWidth - boundingRect.right),
        Bottom: isHidden ? 0 : (window.innerHeight - boundingRect.bottom),
        Height: boundingRect.height,
        Width: boundingRect.width
    };
}
export function addClickOutsideOfElementHandler(callbackTarget, elements, handlerId) {
    if (!elements || !elements.length)
        return false;
    if (handlerId in _clickOutsideOfElementHandlers) {
        removeClickOutsideOfElementHandler(handlerId);
    }
    const callback = (e) => {
        if (e.target) {
            for (var i = 0; i < elements.length; i++) {
                if (elements[i].contains(e.target)) {
                    return;
                }
            }
            callbackTarget.invokeMethod("HandleClickOutsideOfElementCallback", handlerId);
            removeClickOutsideOfElementHandler(handlerId);
        }
    };
    _clickOutsideOfElementHandlers[handlerId] = callback;
    window.addEventListener("click", callback);
    return true;
}
export function removeClickOutsideOfElementHandler(handlerId) {
    if (handlerId in _clickOutsideOfElementHandlers) {
        window.removeEventListener("click", _clickOutsideOfElementHandlers[handlerId]);
        delete _clickOutsideOfElementHandlers[handlerId];
        return true;
    }
    return false;
}
//# sourceMappingURL=ElementService.js.map