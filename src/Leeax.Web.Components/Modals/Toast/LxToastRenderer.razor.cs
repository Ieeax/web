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
                .AddMultiple(ClassName)
                .Add("pos-upper", ToastService.ToastPosition == ToastPosition.UpperLeft 
                    || ToastService.ToastPosition == ToastPosition.UpperCenter 
                    || ToastService.ToastPosition == ToastPosition.UpperRight)
                .Add("pos-lower", ToastService.ToastPosition == ToastPosition.LowerLeft 
                    || ToastService.ToastPosition == ToastPosition.LowerCenter
                    || ToastService.ToastPosition == ToastPosition.LowerRight)
                .Add("pos-left", ToastService.ToastPosition == ToastPosition.LowerLeft 
                    || ToastService.ToastPosition == ToastPosition.UpperLeft)
                .Add("pos-center", ToastService.ToastPosition == ToastPosition.LowerCenter 
                    || ToastService.ToastPosition == ToastPosition.UpperCenter)
                .Add("pos-right", ToastService.ToastPosition == ToastPosition.LowerRight 
                    || ToastService.ToastPosition == ToastPosition.UpperRight));
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // If a toasts gets changed re-render component
            RenderService.StateChanged += StateHasChanged;
        }

        public void Dispose() => RenderService.StateChanged -= StateHasChanged;

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private IToastRenderService RenderService { get; set; }
    }
}