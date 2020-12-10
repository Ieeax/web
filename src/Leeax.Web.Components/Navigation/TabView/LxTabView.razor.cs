using Leeax.Web.Builders;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Navigation
{
    public partial class LxTabView : IContext
    {
        public const string ClassName = "lx-tabview";

        private readonly List<LxTab> _tabs = new List<LxTab>();

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName);
        }

        private RenderFragment? GetActiveRenderFragment()
        {
            if (_tabs != null)
            {
                foreach (var curTab in _tabs)
                {
                    if (curTab.Key == ActiveKey)
                    {
                        return curTab.ChildContent;
                    }
                }
            }

            return null;
        }

        public void AddChild(ComponentBase component)
        {
            _tabs.Add((LxTab)component);

            StateHasChanged();
        }

        public void RemoveChild(ComponentBase component)
        {
            _tabs.RemoveAll(x => x == component);

            StateHasChanged();
        }

        #region Parameters

        /// <summary>
        /// Gets or sets the active key. This determines the shown tab.
        /// </summary>
        [Parameter]
        public string? ActiveKey { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        #endregion
    }
}