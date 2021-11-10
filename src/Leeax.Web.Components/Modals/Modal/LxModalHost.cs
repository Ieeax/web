using Leeax.Web.Components.Abstractions;
using Leeax.Web.Components.Presentation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Leeax.Web.Components.Modals
{
    public class LxModalHost : ComponentBase, IDisposable
    {
        public const string ClassNameBackground = "lx-modal-background";

        private readonly BackwardElementReference _modalReference = new BackwardElementReference();
        private ModalState? _state;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            if (State != null)
            {
                builder.OpenComponent<CascadingValue<ModalState>>(0);
                builder.AddAttribute(1, nameof(CascadingValue<ModalState>.Value), State);
                builder.AddAttribute(2, nameof(CascadingValue<ModalState>.IsFixed), true);
                builder.AddAttribute(3, nameof(CascadingValue<ModalState>.ChildContent), (RenderFragment)(builder2 =>
                {
                    builder2.OpenComponent(4, typeof(CascadingValue<>).MakeGenericType(State.Model.GetType()));
                    builder2.AddAttribute(5, nameof(CascadingValue<object>.Value), State.Model);
                    builder2.AddAttribute(6, nameof(CascadingValue<object>.IsFixed), true);
                    builder2.AddAttribute(7, nameof(CascadingValue<object>.ChildContent), (RenderFragment)(builder3 =>
                    {
                        builder3.OpenComponent<LxTransition>(8);
                        builder3.AddAttribute(9, nameof(LxTransition.IsActive), State.IsActive);
                        builder3.AddAttribute(10, nameof(LxTransition.Target), _modalReference);
                        builder3.AddAttribute(11, nameof(LxTransition.StateChanged), EventCallback.Factory.Create<TransitionState>(this, State.TransitionStateChanged));
                        builder3.AddAttribute(12, nameof(LxTransition.ChildContent), (RenderFragment)(builder4 =>
                        {
                            builder4.OpenElement(13, "div");
                            builder4.AddAttribute(14, "class", ClassNameBackground);
                            builder4.AddElementReferenceCapture(15, @ref => _modalReference.Current = @ref);
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
        public ModalState? State
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