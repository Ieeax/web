using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Presentation
{
    public partial class Card
    {
        public const string ClassName = "lx-card";
        public const string VariableBackgroundColor = ClassName + "-background";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName);
            builder.AddStyleAttribute(x => x
                .AddProperty("width", Width.ToString()));
        }

        /// <summary>
        /// Gets or sets the width of the card. The default value is 10em.
        /// </summary>
        [Parameter]
        public Dimension Width { get; set; } = new Dimension(10, Unit.EM);

        /// <summary>
        /// Gets or sets the height of the image. The default value is 10em.
        /// </summary>
        [Parameter]
        public Dimension Height { get; set; } = new Dimension(10, Unit.EM);

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        [Parameter]
        public string? ImageSource { get; set; }

        /// <summary>
        /// Gets or sets the overline text.
        /// </summary>
        [Parameter]
        public string? Overline { get; set; }

        /// <summary>
        /// Gets or sets the headline text.
        /// </summary>
        [Parameter]
        public string? Headline { get; set; }

        /// <summary>
        /// Gets or sets the sub-headline text.
        /// </summary>
        [Parameter]
        public string? Subheadline { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        [Parameter]
        public RenderFragment? Footer { get; set; }
    }
}