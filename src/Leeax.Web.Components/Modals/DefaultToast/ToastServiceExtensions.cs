using System.Collections.Generic;

namespace Leeax.Web.Components.Modals
{
    public static class ToastServiceExtensions
    {
        public static void ShowToast(this IToastService service, string? text)
        {
            service.Show(new DefaultToastModel(text, DefaultToastModel.DefaultDisplayTime, ToastIcon.None, null, false));
        }

        public static void ShowToast(this IToastService service, string? text, ToastIcon icon)
        {
            service.Show(new DefaultToastModel(text, DefaultToastModel.DefaultDisplayTime, icon, null, false));
        }

        public static void ShowToast(this IToastService service, string? text, int displayTime)
        {
            service.Show(new DefaultToastModel(text, displayTime, ToastIcon.None, null, false));
        }

        public static void ShowToast(this IToastService service, string? text, int displayTime, ToastIcon icon)
        {
            service.Show(new DefaultToastModel(text, displayTime, icon, null, false));
        }

        public static void ShowToast(this IToastService service, string? text, IEnumerable<ToastButton>? buttons)
        {
            service.Show(new DefaultToastModel(text, DefaultToastModel.DefaultDisplayTime, ToastIcon.None, buttons, false));
        }

        public static void ShowToast(this IToastService service, string? text, IEnumerable<ToastButton>? buttons, bool stacked)
        {
            service.Show(new DefaultToastModel(text, DefaultToastModel.DefaultDisplayTime, ToastIcon.None, buttons, stacked));
        }

        public static void ShowToast(this IToastService service, string? text, ToastIcon icon, IEnumerable<ToastButton>? buttons)
        {
            service.Show(new DefaultToastModel(text, DefaultToastModel.DefaultDisplayTime, icon, buttons, false));
        }

        public static void ShowToast(this IToastService service, string? text, ToastIcon icon, IEnumerable<ToastButton>? buttons, bool stacked)
        {
            service.Show(new DefaultToastModel(text, DefaultToastModel.DefaultDisplayTime, icon, buttons, stacked));
        }

        public static void ShowToast(this IToastService service, string? text, int displayTime, ToastIcon icon, IEnumerable<ToastButton>? buttons)
        {
            service.Show(new DefaultToastModel(text, displayTime, icon, buttons, false));
        }

        public static void ShowToast(this IToastService service, string? text, int displayTime, ToastIcon icon, IEnumerable<ToastButton>? buttons, bool stacked)
        {
            service.Show(new DefaultToastModel(text, displayTime, icon, buttons, stacked));
        }
    }
}