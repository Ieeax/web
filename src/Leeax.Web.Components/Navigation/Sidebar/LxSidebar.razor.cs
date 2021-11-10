using Leeax.Web.Builders;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Navigation
{
    public partial class LxSidebar
    {
        public const string ClassName = "lx-sidebar";

        private readonly List<ComponentBase> _items = new List<ComponentBase>();

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName, ClassNames.FlexColumn);
            builder.AddStyleAttribute(x => x
                .AddProperty("width", Width.ToString()));
        }

        public void AddChild(ComponentBase component)
        {
            if (component is ISidebarItem item)
            {
                item.ActiveChanged += OnActiveKeyChanged;
            }

            _items.Add(component);

            StateHasChanged();
        }

        public void RemoveChild(ComponentBase component)
        {
            if (component is ISidebarItem item)
            {
                item.ActiveChanged -= OnActiveKeyChanged;
            }

            _items.RemoveAll(x => x == component);

            StateHasChanged();
        }

        private void OnActiveKeyChanged(object? sender, string? key)
        {
            ActiveKey = key;
            ActiveKeyChanged.InvokeAsync(key);

            StateHasChanged();
        }

        #region Parameters

        /// <summary>
        /// Gets or sets the active key. This determines the active item.
        /// </summary>
        [Parameter]
        public string? ActiveKey { get; set; }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="ActiveKey"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<string> ActiveKeyChanged { get; set; }

        /// <summary>
        /// Gets or sets the width of the sidebar.
        /// </summary>
        [Parameter]
        public Dimension Width { get; set; }

        [Parameter]
        public RenderFragment? Header { get; set; }

        [Parameter]
        public RenderFragment? Content { get; set; }

        [Parameter]
        public RenderFragment? Footer { get; set; }
        #endregion
    }
}