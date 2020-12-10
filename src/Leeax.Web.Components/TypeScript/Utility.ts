enum Interaction {
    None,
    Hover = 1 << 1,
    Active = 1 << 2
}

const CLASS_ACTIVE = "lx-active";
const CLASS_HOVER = "lx-hover";

let _initialized: boolean;
let _elementHovered: Element;
let _elementActive: Element;

function onMouseDownTest(e: MouseEvent) {

    const path = e.composedPath();
    for (var i = 0; i < path.length; i++) {

        if ((getInteraction(path[i]) & Interaction.Active) === Interaction.Active) {

            _elementActive = path[i] as Element;
            _elementActive.classList.add(CLASS_ACTIVE);

            window.removeEventListener("mousedown", onMouseDownTest);
            window.addEventListener("mouseup", onMouseUpTest);
            return;
        }
    }
}

function onMouseUpTest(e: MouseEvent) {

    _elementActive.classList.remove(CLASS_ACTIVE);
    _elementActive = null;

    window.removeEventListener("mouseup", onMouseUpTest);
    window.addEventListener("mousedown", onMouseDownTest);
}

function onMouseEnterTest(e: MouseEvent): void {

    const path = e.composedPath();
    for (var i = 0; i < path.length; i++) {

        if ((getInteraction(path[i]) & Interaction.Hover) === Interaction.Hover) {

            if (_elementHovered != null) {
                _elementHovered.classList.remove(CLASS_HOVER);
            }

            _elementHovered = path[i] as Element;
            _elementHovered.classList.add(CLASS_HOVER);

            window.removeEventListener("mouseenter", onMouseEnterTest);
            window.addEventListener("mouseout", onMouseLeaveTest);
            return;
        }
    }
}

function onMouseLeaveTest(e: MouseEvent) {

    if (_elementHovered !== e.target) return;

    _elementHovered.classList.remove(CLASS_HOVER);
    _elementHovered = null;

    window.removeEventListener("mouseout", onMouseLeaveTest);
    window.addEventListener("mouseover", onMouseEnterTest);
}

function getInteraction(el: EventTarget): Interaction {

    if (!(el instanceof Element)) return;

    const value = el.getAttribute("data-lx-interaction");

    if (value === null) {
        return Interaction.None;
    }

    switch (value) {
        case "1":
            return Interaction.Hover;
        case "2":
            return Interaction.Active;
        case "3":
            return Interaction.Hover | Interaction.Active;
        default:
            return Interaction.None;
    }
}

function init(): void {

    // Ensure that listeners are only attached once
    if (_initialized === true) return;

    _initialized = true;

    window.addEventListener("mousedown", onMouseDownTest);
    window.addEventListener("mouseover", onMouseEnterTest);
}

init();