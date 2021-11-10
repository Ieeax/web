using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Components.Modals
{
    public class LxModalFooter : LxComponentBase
    {
        public const string ClassName = "lx-modal-footer";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName, ClassNames.FlexRow, "p-2", "mt-3");
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            
            builder.OpenElement(0, "footer");
            builder.AddMultipleAttributes(1, AttributeSet);
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}