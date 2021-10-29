using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Drawing;

namespace Leeax.Web.Components.Presentation
{
    public partial class LxLoadingSpinner : LxComponentBase
    {
        public const string ClassName = "lx-loadingspinner";
        public const string VariableColor = ClassName + "-color";

        private Color _color;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "svg");
            builder.AddAttribute(1, "viewBox", "0 0 50 50");
            builder.AddMultipleAttributes(2, AttributeSet!);
            builder.OpenElement(3, "circle");
            builder.AddAttribute(4, "class", "spinner-path");
            builder.AddAttribute(5, "style", GetSpinnerCircleStyle());
            builder.AddAttribute(6, "cx", "25");
            builder.AddAttribute(7, "cy", "25");
            builder.AddAttribute(8, "r", "20");
            builder.AddAttribute(9, "fill", "none");
            builder.AddAttribute(10, "stroke-width", "5");
            builder.CloseElement();
            builder.CloseElement();
        }

        protected override void OnParametersSet()
        {
            _color = StyleContext.GetColorOrDefault(VariableColor, VariableNames.ThemePrimary);
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .Add(ClassName)
                .Add(ClassNames.Active, IsActive));

            builder.AddStyleAttribute(x => x
                .AddProperty("height", Size.ToString())
                .AddProperty("width", Size.ToString()));
        }
        
        private string? GetSpinnerCircleStyle()
        {
            return CssBuilder.Create()
                .AddProperty("stroke", _color.ToHexStr())
                .Build();
        }

        /// <summary>
        /// Gets or sets whether the spinner gets displayed.
        /// </summary>
        [Parameter]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the size of the spinner.
        /// </summary>
        [Parameter]
        public Dimension Size { get; set; } = new Dimension(2, Unit.EM);
    }
}