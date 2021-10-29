using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Components.Presentation
{
    public class LxSkeleton : LxComponentBase
    {
        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            base.BuildAttributeSet(builder);

            builder.AddAttribute("role", "progressbar");
            builder.AddAriaAttribute("busy", "true");
            
            builder.AddClassAttribute("lx-skeleton", Shape switch
            {
                SkeletonShape.Rectangle => "lx-skeleton-rect",
                SkeletonShape.Ellipsis => "lx-skeleton-ellipsis",
                SkeletonShape.Pill => "lx-skeleton-pill",
                _ => "lx-skeleton-text", // default
            });
            
            builder.AddStyleAttribute(x => x
                .AddHeight(Height)
                .AddWidth(Width));
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AttributeSet);
            builder.CloseElement();
        }

        [Parameter]
        public Dimension Height { get; set; }

        [Parameter]
        public Dimension Width { get; set; }

        [Parameter]
        public SkeletonShape Shape { get; set; }
    }
}