
export function create(dotNetRef: any): TimePickerWheel {
    return new TimePickerWheel(dotNetRef);
}

class TimePickerWheel {

    constructor(dotNetRef: any) {
        this._dotNetRef = dotNetRef;
    }

    private _dotNetRef: any;
    private _wheelCallback: (e: WheelEvent) => void;
    private _wheelTarget: HTMLElement;

    public addOrUpdateWheelListener(element: HTMLElement): void {

        if (this._wheelCallback) {
            this.removeWheelListener();
        }

        this._wheelTarget = element;
        this._wheelCallback = (e: WheelEvent) => {
            e.preventDefault();
            this._dotNetRef.invokeMethodAsync("HandleWheelEventCallback", e.deltaY);
        }

        this._wheelTarget.addEventListener("wheel", this._wheelCallback, { passive: false });
    }

    public removeWheelListener(): void {

        if (this._wheelCallback) {

            this._wheelTarget.removeEventListener("wheel", this._wheelCallback);

            this._wheelTarget = null;
            this._wheelCallback = null;
        }
    }
}