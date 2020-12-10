using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Modals
{
    public partial class LxModalHeader
    {
        public const string ClassName = "lx-modal-header";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            base.BuildAttributeSet(builder);

            builder.AddClassAttribute(ClassName, ClassNames.FlexRow, ClassNames.FlexVerticalCenter);
        }

        /// <summary>
        /// Gets or sets the icon source.
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the header title.
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the callback to execute whenever the "close"-button was clicked.
        /// </summary>
        [Parameter]
        public EventCallback Closed { get; set; }
    }
}