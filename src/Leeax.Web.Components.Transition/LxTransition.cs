using Leeax.Web.Components.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Presentation
{
    public class LxTransition : ComponentBase, IAsyncDisposable
    {
        public const string ModulePath = "./_content/Leeax.Web.Components.Transition/Transition.min.js";
        public const string DefaultClassPrefix = "t";

        private IJSInProcessObjectReference? _jsReference;
        private ElementReference _target;

        private TransitionState _transitionState;
        private bool _shouldRender;
        private bool _isActive;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            if (!_shouldRender 
                || _jsReference == null) // Prevent rendering until javascript is loaded
            {
                return;
            }

            if (Target != null)
            {
                builder.AddContent(0, ChildContent);
            }
            else
            {
                builder.OpenElement(1, "div");
                builder.AddAttribute(2, "class", "lx-transition");
                builder.AddElementReferenceCapture(3, @ref => _target = @ref);
                builder.AddContent(4, ChildContent);
                builder.CloseElement();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _jsReference = await JSRuntime
                    .InvokeAsync<IJSInProcessObjectReference>("import", ModulePath);

                StateHasChanged();
                return;
            }

            // Ensure that reference was resolved (important!)
            // Lifecycle method could be called twice very fast:
            // If that's the case the second call (is not "firstRender") would 
            // try to call a function on the js-object which is not yet resolved
            if (_jsReference == null)
            {
                return;
            }

            if (Target != null)
            {
                _target = Target.Current;
            }
            
            if (_target.Id == null)
            {
                return;
            }

            if (_isActive)
            {
                if (_transitionState == TransitionState.Hidden)
                {
                    _transitionState = TransitionState.Visible;

                    RegisterCallback("enter");
                    await StateChanged.InvokeAsync(true);
                }

                return;
            }

            if (_transitionState == TransitionState.Visible)
            {
                _transitionState = TransitionState.Hidden;

                RegisterCallback("leave");
            }
        }

        // This method can only be called when the js-references are resolved
        private void RegisterCallback(string action)
        {
            _jsReference!.InvokeVoid(
                "registerCallback",
                DotNetObjectReference.Create(this),
                _target,
                Hooks,
                action,
                ClassPrefix ?? DefaultClassPrefix,
                action == "leave" ? Duration.DurationLeave : Duration.DurationEnter);
        }

        public ValueTask DisposeAsync()
        {
            return _jsReference == null
                ? ValueTask.CompletedTask
                : _jsReference.DisposeAsync();
        }

        /// <summary>
        /// Gets called after a transition or animation finished.
        /// </summary>
        /// <param name="property">If the type is set to <see cref="TransitionType.Transition"/> this is set to the property which finished transitioning, else this is set to the animation name.</param>
        [JSInvokable]
        public void HandleCallback(string? property)
        {
            _shouldRender = _transitionState != TransitionState.Hidden;
            StateChanged.InvokeAsync(_shouldRender);

            // Trigger re-render to remove content
            StateHasChanged();
        }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        #region Parameters
        /// <summary>
        /// Gets or sets the transition type. The default value is <see cref="TransitionType.Transition"/>. 
        /// If you're using a CSS-animation this have to be set to <see cref="TransitionType.Animation"/>.
        /// </summary>
        [Parameter]
        public TransitionType Type { get; set; }

        /// <summary>
        /// Gets or sets a reference to an javascript object which can contain hooks.
        /// </summary>
        [Parameter]
        public IJSObjectReference? Hooks { get; set; }

        /// <summary>
        /// Gets or sets whether the <see cref="ChildContent"/> gets rendered.
        /// </summary>
        [Parameter]
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive == value)
                {
                    return;
                }

                _isActive = value;

                if (value)
                {
                    // If false, the component will stop rendering after receiving the "transitionend/animationend" callback
                    _shouldRender = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the class prefix. The default value is "t".
        /// </summary>
        [Parameter]
        public string? ClassPrefix { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="BackwardElementReference"/> which specifies the (child-) element to apply the transition to. If no target is specified, an additional &lt;div&gt; tag gets wrapped around the supplied <see cref="ChildContent"/>. 
        /// If this parameter is set, no &lt;div&gt; tag will be rendered and the transition classes are directly applied to the specified element.
        /// </summary>
        [Parameter]
        public BackwardElementReference? Target { get; set; }

        /// <summary>
        /// Gets or sets a custom transition duration. By default this is determined trough the CSS property.
        /// </summary>
        [Parameter]
        public TransitionDuration Duration { get; set; }

        /// <summary>
        /// Gets or sets the content for the transition.
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever the transition state changed. (<see langword="true"/> equals active)
        /// </summary>
        [Parameter]
        public EventCallback<bool> StateChanged { get; set; }
        #endregion
    }
}