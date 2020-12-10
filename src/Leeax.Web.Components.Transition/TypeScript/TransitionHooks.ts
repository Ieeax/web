export type TransitionHooks = {
    beforeEnter?: (element: HTMLElement) => void,
    afterEnter?: (element: HTMLElement) => void,
    enterCancelled?: (element: HTMLElement) => void,
    beforeLeave?: (element: HTMLElement) => void,
    afterLeave?: (element: HTMLElement) => void,
    leaveCancelled?: (element: HTMLElement) => void,
}