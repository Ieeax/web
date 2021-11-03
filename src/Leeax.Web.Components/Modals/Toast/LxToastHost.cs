using Leeax.Web.Components.Abstractions;
using Leeax.Web.Components.Presentation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Leeax.Web.Components.Modals
{
    public class LxToastHost : ComponentBase, IDisposable
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
                builder.OpenComponent<CascadingValue<ToastState>>(0);
                builder.AddAttribute(1, nameof(CascadingValue<ToastState>.Value), State);
                builder.AddAttribute(2, nameof(CascadingValue<ToastState>.IsFixed), true);
                builder.AddAttribute(3, nameof(CascadingValue<ToastState>.ChildContent), (RenderFragment)(builder2 =>
                {
                    builder2.OpenComponent(4, typeof(CascadingValue<>).MakeGenericType(State.Model.GetType()));
                    builder2.AddAttribute(5, nameof(CascadingValue<object>.Value), State.Model);
                    builder2.AddAttribute(6, nameof(CascadingValue<object>.IsFixed), true);
                    builder2.AddAttribute(7, nameof(CascadingValue<object>.ChildContent), (RenderFragment)(builder3 =>
                    {
                        builder3.OpenComponent<LxTransition>(8);
                        builder3.AddAttribute(9, nameof(LxTransition.IsActive), State.IsActive);
                        builder3.AddAttribute(10, nameof(LxTransition.Target), _toastReference);
                        builder3.AddAttribute(11, nameof(LxTransition.StateChanged), EventCallback.Factory.Create<TransitionState>(this, State.TransitionStateChanged));
                        builder3.AddAttribute(12, nameof(LxTransition.ChildContent), (RenderFragment)(builder4 =>
                        {
                            builder4.OpenElement(13, "div");
                            builder4.AddAttribute(14, "class", ClassName + " lx-elevation-l5");
                            builder4.AddElementReferenceCapture(15, @ref => _toastReference.Current = @ref);
                            builder4.OpenComponent(16, State.ComponentType);
                            builder4.CloseComponent();
                            builder4.CloseElement();
                        }));
                        builder3.CloseComponent();
                    }));
                    builder2.CloseComponent();
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