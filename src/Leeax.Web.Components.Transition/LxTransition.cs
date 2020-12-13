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
        public const string DefaultName = "t";
        public const string ClassNameWrapper = "lx-transition";

        private IJSObjectReference? _jsReference;
        private IJSInProcessObjectReference? _jsInProcessReference;
        private ElementReference _target;
        private TransitionState _state = TransitionState.Left;
        private bool _renderContent;
        private bool _isActive;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (!_renderContent
                || _jsReference == null) // Prevent rendering content until js module is loaded
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
                builder.AddAttribute(2, "class", ClassNameWrapper);
                builder.AddElementReferenceCapture(3, @ref => _target = @ref);
                builder.AddContent(4, ChildContent);
                builder.CloseElement();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // Resolve (import) required javascript
                // Note: We want to use synchronous js calls whenever possible (for performance)
                if (JSRuntime is IJSInProcessRuntime)
                {
                    _jsInProcessReference = await JSRuntime.InvokeAsync<IJSInProcessObjectReference>("import", ModulePath);
                    _jsReference = _jsInProcessReference;
                }
                else
                {
                    _jsReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import", ModulePath);
                }

                // Trigger re-render after javascript was resolved
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

            // When a target element was supplied we want to use it
            if (Target != null
                && Target.HasValue)
            {
                _target = Target.Current;
            }

            // Check whether the target element exists in DOM
            if (_target.Id == null)
            {
                return;
            }

            if (_isActive)
            {
                // Check if we need to show the content
                if (_state == TransitionState.Leaving
                    || _state == TransitionState.Left)
                {
                    _state = TransitionState.Entering; // Set correct state before registering js callback

                    // When possible use the synchronous js call
                    if (_jsInProcessReference != null) RegisterCallback();
                    else await RegisterCallbackAsync();

                    await StateChanged.InvokeAsync(_state);
                }

                return;
            }

            // Check if we need to hide the content
            if (_state == TransitionState.Entering
                || _state == TransitionState.Entered)
            {
                _state = TransitionState.Leaving; // Set correct state before registering js callback

                // When possible use the synchronous js call
                if (_jsInProcessReference != null) RegisterCallback();
                else await RegisterCallbackAsync();

                await StateChanged.InvokeAsync(_state);
            }
        }

        // This method can only be called when the js-references are resolved
        private ValueTask RegisterCallbackAsync()
            => _jsReference!.InvokeVoidAsync("registerCallback", GetCallbackArguments());

        // This method can only be called when the js-references are resolved
        private void RegisterCallback()
            => _jsInProcessReference!.InvokeVoid("registerCallback", GetCallbackArguments());

        // Important: To ensure the correct arguments are generated, 
        // the field "_state" and "_target" have to be set correctly
        private object?[] GetCallbackArguments()
        {
            return new object?[]
            {
                DotNetObjectReference.Create(this),
                _target,
                Hooks,
                _state == TransitionState.Entering ? "enter" : "leave", // State should be either "Entering" or "Leaving"
                Name ?? DefaultName,
                Type == TransitionType.Animation ? "animation" : "transition",
                _state == TransitionState.Entering ? Duration.DurationEnter : Duration.DurationLeave
            };
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
        public async Task HandleCallbackAsync(string? property)
        {
            _state = _state switch
            {
                TransitionState.Entering => TransitionState.Entered,
                TransitionState.Leaving => TransitionState.Left,
                _ => throw new ApplicationException($"Invalid internal {nameof(TransitionState)} \"{_state}\". Exception may occur when calling method \"{nameof(HandleCallbackAsync)}\" manually.")
            };

            // Decide whether the content should be still rendered
            _renderContent = _state == TransitionState.Entered;

            await StateChanged.InvokeAsync(_state);

            // Trigger re-render to remove content
            StateHasChanged();
        }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        #region Parameters
        /// <summary>
        /// Gets or sets the transition type. The default value is <see cref="TransitionType.Transition"/>. 
        /// If you're using a CSS-animation this value has to be set to <see cref="TransitionType.Animation"/>.
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
                    _renderContent = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the transition name. Will be used as prefix for all transition classes. 
        /// The default value is <see cref="DefaultName"/>.
        /// </summary>
        [Parameter]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="BackwardElementReference"/> which specifies the (child-) element to apply the transition to. If no target is specified, an additional &lt;div&gt; tag gets wrapped around the supplied <see cref="ChildContent"/>. 
        /// If this parameter is set, no &lt;div&gt; tag will be rendered and the transition classes are directly applied to the specified element.
        /// </summary>
        [Parameter]
        public BackwardElementReference? Target { get; set; }

        /// <summary>
        /// Gets or sets a custom transition duration. By default the value gets determined trough the duration of the CSS transition/animation.
        /// </summary>
        [Parameter]
        public TransitionDuration Duration { get; set; }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever the transition state changed.
        /// </summary>
        [Parameter]
        public EventCallback<TransitionState> StateChanged { get; set; }

        /// <summary>
        /// Gets or sets the content of the transition.
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        #endregion
    }
}