using Leeax.Web.Builders;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Components.Presentation
{
    public class LxFrame : LxComponentBase
    {
        public const string ClassName = "lx-frame";

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AttributeSet!);
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, ClassNames.Border, ClassNames.BorderRounded, ClassNames.OverflowHidden)
                .AddElevation(Elevation));

            builder.Merge(AdditionalAttributes);
        }

        /// <summary>
        /// Gets or sets the elevation level. (none = 0, max = 6)
        /// </summary>
        [Parameter]
        public int Elevation { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object?>? AdditionalAttributes { get; set; }
    }
}