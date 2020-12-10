using Leeax.Web.Components.Abstractions;
using Leeax.Web.Components.Presentation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Leeax.Web.Components.Modals
{
    public class LxToast : ComponentBase, IDisposable
    {
        public const string ClassName = "lx-toast";
        public const string VariableBackgroundColor = ClassName + "-background";

        private readonly BackwardElementReference _toastReference = new BackwardElementReference();
        private ToastState? _state;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            if (State != null)
            {
                builder.OpenComponent<LxTransition>(0);
                builder.AddAttribute(1, nameof(LxTransition.IsActive), State.IsActive);
                builder.AddAttribute(2, nameof(LxTransition.Target), _toastReference);
                builder.AddAttribute(3, nameof(LxTransition.StateChanged), EventCallback.Factory.Create<bool>(this, State.TransitionStateChanged));
                builder.AddAttribute(4, nameof(LxTransition.ChildContent), (RenderFragment)(builder2 =>
                {
                    builder2.OpenElement(5, "div");
                    builder2.AddAttribute(6, "class", ClassName + " lx-elevation-l5");
                    builder2.AddElementReferenceCapture(7, @ref => _toastReference.Current = @ref);
                    builder2.OpenComponent(8, State.ComponentType);
                    builder2.AddAttribute(9, "Model", State.Model);
                    builder2.CloseComponent();
                    builder2.CloseElement();
                }));
                builder.CloseComponent();
            }
        }

        public void Dispose()
        {
            if (_state != null)
            {
                _state.StateChanged -= StateHasChanged;
            }
        }

        [Parameter]
        public ToastState? State
        {
            get => _state;
            set
            {
                if (_state != null)
                {
                    // Unregister event
                    _state.StateChanged -= StateHasChanged;
                }

                _state = value;

                if (_state != null)
                {
                    // Register new event
                    _state.StateChanged += StateHasChanged;
                }
            }
        }
    }
}