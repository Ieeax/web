using Leeax.Web.Builders;
using System;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Modals
{
    public partial class LxToastRenderer : IDisposable
    {
        public const string ClassName = "lx-toastrenderer";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .Add(ClassName)
                .Add(ClassNames.VerticalAlignmentTop, ToastService.ToastPosition is ToastPosition.UpperLeft or ToastPosition.UpperCenter or ToastPosition.UpperRight)
                .Add(ClassNames.VerticalAlignmentBottom, ToastService.ToastPosition is ToastPosition.LowerLeft or ToastPosition.LowerCenter or ToastPosition.LowerRight)
                .Add(ClassNames.HorizontalAlignmentLeft, ToastService.ToastPosition is ToastPosition.LowerLeft or ToastPosition.UpperLeft)
                .Add(ClassNames.HorizontalAlignmentCenter, ToastService.ToastPosition is ToastPosition.LowerCenter or ToastPosition.UpperCenter)
                .Add(ClassNames.HorizontalAlignmentRight, ToastService.ToastPosition is ToastPosition.LowerRight or ToastPosition.UpperRight));
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // If a toasts gets changed re-render component
            RenderService.StateChanged += StateHasChanged;
        }

        public void Dispose() => RenderService.StateChanged -= StateHasChanged;

        [Inject]
        private IToastService ToastService { get; set; } = null!;

        [Inject]
        private IToastRenderService RenderService { get; set; } = null!;
    }
}