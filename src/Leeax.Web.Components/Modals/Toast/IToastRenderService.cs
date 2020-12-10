using System;
using System.Collections.Generic;

namespace Leeax.Web.Components.Modals
{
    public interface IToastRenderService
    {
        event Action? StateChanged;

        /// <summary>
        /// Gets the active toasts.
        /// </summary>
        IEnumerable<ToastState> ActiveToasts { get; }
    }
}