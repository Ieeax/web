using System.Collections.Generic;

namespace Leeax.Web.Components.Modals
{
    public static class ToastServiceExtensions
    {
        public static void ShowToast(this IToastService service, string? text)
        {
            service.Show(new DefaultToastModel(text, ToastIcon.None, null, false));
        }

        public static void ShowToast(this IToastService service, string? text, int displayTime)
        {
            service.Show(new DefaultToastModel(text, ToastIcon.None, null, false), displayTime);
        }

        public static void ShowToast(this IToastService service, string? text, ToastIcon icon)
        {
            service.Show(new DefaultToastModel(text, icon, null, false));
        }

        public static void ShowToast(this IToastService service, string? text, ToastIcon icon, int displayTime)
        {
            service.Show(new DefaultToastModel(text, icon, null, false), displayTime);
        }

        public static void ShowToast(this IToastService service, string? text, IEnumerable<ToastButton>? buttons)
        {
            service.Show(new DefaultToastModel(text, ToastIcon.None, buttons, false));
        }

        public static void ShowToast(this IToastService service, string? text, IEnumerable<ToastButton>? buttons, bool stacked)
        {
            service.Show(new DefaultToastModel(text, ToastIcon.None, buttons, stacked));
        }

        public static void ShowToast(this IToastService service, string? text, ToastIcon icon, IEnumerable<ToastButton>? buttons)
        {
            service.Show(new DefaultToastModel(text, icon, buttons, false));
        }

        public static void ShowToast(this IToastService service, string? text, ToastIcon icon, IEnumerable<ToastButton>? buttons, bool stacked)
        {
            service.Show(new DefaultToastModel(text, icon, buttons, stacked));
        }

        public static void ShowToast(this IToastService service, string? text, ToastIcon icon, IEnumerable<ToastButton>? buttons, int displayTime)
        {
            service.Show(new DefaultToastModel(text, icon, buttons, false), displayTime);
        }

        public static void ShowToast(this IToastService service, string? text, ToastIcon icon, IEnumerable<ToastButton>? buttons, bool stacked, int displayTime)
        {
            service.Show(new DefaultToastModel(text, icon, buttons, stacked), displayTime);
        }
    }
}