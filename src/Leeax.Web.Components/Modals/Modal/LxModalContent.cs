using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Components.Modals
{
    public class LxModalContent : LxComponentBase
    {
        public const string ClassName = "lx-modal-content";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName, ClassNames.ScrollbarThin, "flex1", "px-3");
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            
            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AttributeSet);
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}