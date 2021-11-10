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

            builder.AddClassAttribute(ClassName, ClassNames.FlexRow, ClassNames.FlexVerticalCenter, "p-3");
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
        /// Gets or sets whether a "close" button is rendered.
        /// The default value is <see langword="true"/>.
        /// </summary>
        [Parameter]
        public bool AllowClose { get; set; } = true;
        
        [CascadingParameter]
        public ModalState? Context { get; set; }
    }
}