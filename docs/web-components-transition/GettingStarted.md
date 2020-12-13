# Leeax.Web.Components.Transition
Provides a transition component which allows to add entering/leaving transitions for any element or component. (similar to the transitions from Vue.js)

## Getting Started
To apply a transition, the element or component to transition/animate has to be wrapped in the `LxTransition` component. When we now want to display the actual content, we just need to set the `IsActive` parameter to `true`. To hide it again, we set it back to `false`.
```html
@using Leeax.Web.Components.Presentation

<LxTransition IsActive="IsActive">
    <LxButton Text="Click me" />
</LxTransition>

@code {
    public bool IsActive { get; set; }
}
```
If your testing this, you've probably noticed that nothing actually happens yet when toggling the `IsActive` parameter. That is because the `LxTransition` component only applies different transition classes (see further down for more info), which then we can use to apply our own CSS transitions and animations.

For a simple fade-in/out transition, we need to add the following CSS in our component or stylesheet:
```css
.t-enter,
.t-leave-to {
    transform: translateX(10px);
    opacity: 0;
}

.t-enter-to,
.t-leave-to {
    transition: all 1s;
}
```
Now, if we change the `IsActive` parameter, the `LxButton` actual fades in and out like anticipated.

## Documentation

### Transition Root-Element
By default, the `LxTransition` component wraps an `<div class="lx-transition">` element around the supplied `ChildContent`. In this case, all transition classes will be applied to the `<div>` element and **not** to the actual content.

However, this is not always desirable, as it can affect the structure of the HTML and CSS rules may not be applied correctly. To fix this, we need to capture an `ElementReference` of the element to apply the classes to. For this we're using the `BackwardElementReference` class, which passes the inner `ElementReference` to an outer component:
```html
<LxTransition Target="_target">
    <span @ref="_target.Current">...</span>
</LxTransition>

@code {
    private BackwardElementReference _target = new BackwardElementReference();
}
```
After this, the transition classes are directly applied to the `<span>` element and no wrapper element is rendered.

### Transition Classes
There are a total of 6 different classes, which we can use to create our transition with:
1. `t-enter`: Initial state when entering. Added after the first render-cycle of Blazor (which inserts the element), removed one frame after.

2. `t-enter-active`: Active state when entering. Applied during the entire entering phase. Added after the first render-cycle of Blazor (which inserts the element), removed when transition/animation finishes.

3. `t-enter-to`: Ending state for entering. Added at the same time `v-enter` is removed, removed when transition/animation finishes.

4. `t-leave`: Initial state when leaving. Added immediately when a leaving transition is triggered, removed after one frame.

5. `t-leave-active`: Active state when leaving. Applied during the entire leaving phase. Added immediately when leave transition is triggered, removed when the transition/animation finishes.

6. `t-leave-to`: Ending state for leaving. Added at the same time `v-leave` is removed, removed when transition/animation finishes.

By default, all classes are prefixed with `t`. This can be changed by setting the `Name` parameter of the `LxTransition` component.

> **Important**: Enter transitions should be defined trough `t-enter-to` and not `t-enter-active` (as in Vue.js). Since Blazor renders the element initially (we have no control with which class the element is inserted), `t-enter-active` will be set after the element is already inserted. This has the consequence, that the transition gets also applied when changing the state of the element with no class to the state with a class `t-enter` / `t-enter-active`.
>
> Therefore the following example does not work:
> ```css
> .t-enter {
>     opacity: 0;
> }
> .t-enter-active { /* Correct would be "t-enter-to" */
>     transition: all 1s;
>}
>```

### CSS Animations
If you want to use a CSS animation and the `LxTransition` component should auto-detect when it finished, the parameter `Type` has to be set to `TransitionType.Animation`:
```html
<LxTransition Type="TransitionType.Animation">...</LxTransition>
```

###  Explicit Transition Durations
In most cases, the `LxTransition` component can automatically figure out when the transition has finished. By default, the component waits for the first `transitionend` or `animationend` event on the root transition element (based on the `Type` parameter). If you want to define the duration manually, you can set the `Duration` parameter.
```html
<LxTransition Duration="new TransitionDuration(1500, 2000)">...</LxTransition> @* Enter = 1500ms, Leave = 2000ms *@

@* Can be also defined like this: *@
<LxTransition Duration="1500">...</LxTransition> @* Enter + Leave = 1500ms *@
<LxTransition Duration="(1500, 2000)">...</LxTransition> @* Enter = 1500ms, Leave = 2000ms *@
```

### JavaScript Hooks
If required, you can also define javascript hooks:
```js
function getTransitionHooks() {
    return {
        beforeEnter: function (el) {
            // ...
        },
        afterEnter: function (el) {
            // ...
        },
        enterCancelled: function (el) {
            // ...
        },
        beforeLeave: function (el) {
            // ...
        },
        afterLeave: function (el) {
            // ...
        },
        leaveCancelled: function (el) {
            // ...
        }
    };
}
```

To pass these hooks to a `LxTransition` component, you'll first need to create an `IJSObjectReference` to them:
```cs
private IJSObjectReference _jsHooks;

protected override async Task OnInitializedAsync()
{
    _jsHooks = await JSRuntime.InvokeAsync<IJSObjectReference>("getTransitionHooks");
}
```
After that, you can pass the hooks to the component:
```html
<LxTransition Hooks="_jsHooks">...</LxTransition>
```

## Dependencies
This project as well all dependencies are based on .NET 5.0.
- Leeax.Web.Components.Abstractions

## Issues
Feel free to create an issue when encountering any problems.