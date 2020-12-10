using Leeax.Web.Builders;
using Leeax.Web.Components.Abstractions;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Presentation
{
    public partial class LxExpandable
    {
        public const string ClassName = "lx-expandable";

        private readonly BackwardElementReference _contentReference = new BackwardElementReference();

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName);
        }

        /// <summary>
        /// Gets or sets whether the component is expanded.
        /// </summary>
        [Parameter]
        public bool IsExpanded { get; set; }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="Items"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<bool> IsExpandedChanged { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}