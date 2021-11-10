using System;
using System.Threading.Tasks;
using Leeax.Web.Builders;
using Leeax.Web.Components.Abstractions;
using Leeax.Web.Components.DOM;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Components.Modals
{
    public class LxModal : LxComponentBase
    {
        public const string ClassName = "lx-modal";
        
        private ElementReference _modalReference;
        private long _callbackId = -1;
        
        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, "overflow-hidden", "flex-col", "lx-elevation-l5")
                .AddAlignment(Alignment));
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            
            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AttributeSet);
            builder.AddElementReferenceCapture(2, element => _modalReference = element);
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_callbackId > -1)
            {
                // Ensure that callback is cleaned up
                await ElementService.RemoveClickOutsideOfElementHandlerAsync(_callbackId);
                _callbackId = -1;
            }

            if (EnableClickOutside
                && Context != null
                && Context.IsActive)
            {
                // Register callback if user clicks outside of dropdown
                _callbackId = await ElementService.AddClickOutsideOfElementHandlerAsync(
                    new[] { _modalReference }, 
                    () =>
                    {
                        _callbackId = -1;
                        Context.Close();
                    });
            }
        }
        
        [Inject]
        private IElementService ElementService { get; set; } = null!;

        #region Parameters
        
        /// <summary>
        /// Gets or sets the alignment of the modal.
        /// The default value is <see cref="Alignment.Center"/>.
        /// </summary>
        [Parameter]
        public Alignment Alignment { get; set; } = Alignment.Center;

        /// <summary>
        /// Gets or sets whether the modal should closed when the user clicks outside of it.
        /// The default value is <see langword="true"/>.
        /// </summary>
        [Parameter]
        public bool EnableClickOutside { get; set; } = true;
        
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        
        [CascadingParameter]
        public ModalState? Context { get; set; }
        #endregion
    }
}