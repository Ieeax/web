using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Components.Theme
{
    public class LxThemeScope : ComponentBase, IDisposable
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<LxStyleScope>(0);
            builder.AddAttribute(1, nameof(LxStyleScope.Value), ThemeHandler.GetActiveStyle());
            builder.AddAttribute(2, nameof(LxStyleScope.RenderElement), true);
            builder.AddAttribute(3, nameof(LxStyleScope.ChildContent), ChildContent);
            builder.CloseComponent();
        }

        protected override void OnInitialized()
        {
            ThemeHandler.ThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged(object? sender, EventArgs args) => StateHasChanged();

        public void Dispose()
        {
            ThemeHandler.ThemeChanged -= OnThemeChanged;
        }

        [Inject]
        public IThemeHandler ThemeHandler { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}