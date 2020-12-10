using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Navigation
{
    public partial class LxSidebarGroup
    {
        public const string ClassName = "lx-sidebargroup";

        private bool _expandedInitialized;
        private bool _expanded = true;

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .Add(ClassName)
                .Add("expandable", IsExpandable)
                .Add("expanded", _expanded));
        }

        private async Task ToggleStateAsync()
        {
            if (IsExpandable)
            {
                _expanded = !_expanded;
                await IsExpandedChanged.InvokeAsync(_expanded);
            }
        }

        /// <summary>
        /// Gets or set whether the group is expandable. The default value is <see langword="true"/>.
        /// </summary>
        [Parameter]
        public bool IsExpandable { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the group is expanded. The default value is <see langword="true"/>.
        /// </summary>
        [Parameter]
        public bool IsExpanded
        {
            get => _expanded;
            set
            {
                if (!_expandedInitialized)
                {
                    _expanded = value;
                    _expandedInitialized = true;
                }
                else if (IsExpandedChanged.HasDelegate)
                {
                    _expanded = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="IsExpanded"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<bool> IsExpandedChanged { get; set; }

        [Parameter]
        public RenderFragment? Header { get; set; }

        [Parameter]
        public RenderFragment? Content { get; set; }
    }
}