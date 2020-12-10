export function create(dotNetRef) {
    return new TimePickerWheel(dotNetRef);
}
class TimePickerWheel {
    constructor(dotNetRef) {
        this._dotNetRef = dotNetRef;
    }
    addOrUpdateWheelListener(element) {
        if (this._wheelCallback) {
            this.removeWheelListener();
        }
        this._wheelTarget = element;
        this._wheelCallback = (e) => {
            e.preventDefault();
            this._dotNetRef.invokeMethodAsync("HandleWheelEventCallback", e.deltaY);
        };
        this._wheelTarget.addEventListener("wheel", this._wheelCallback, { passive: false });
    }
    removeWheelListener() {
        if (this._wheelCallback) {
            this._wheelTarget.removeEventListener("wheel", this._wheelCallback);
            this._wheelTarget = null;
            this._wheelCallback = null;
        }
    }
}
//# sourceMappingURL=TimePickerWheel.js.map