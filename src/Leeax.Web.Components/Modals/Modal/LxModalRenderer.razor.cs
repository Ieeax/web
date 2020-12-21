using Leeax.Web.Builders;
using System;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Modals
{
    public partial class LxModalRenderer : IDisposable
    {
        public const string ClassName = "lx-modalrenderer";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .Add(ClassName)
                .Add(ClassNames.Active, RenderService.ActiveModal != null));
        }

        protected override void OnInitialized()
        {
            // If a dialog gets changed re-render component
            RenderService.StateChanged += StateHasChanged;
        }

        public void Dispose()
        {
            RenderService.StateChanged -= StateHasChanged;
        }

        [Inject]
        private IModalRenderService RenderService { get; set; }
    }
}