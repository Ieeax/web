using System.Linq;
using System.Collections.Generic;
using System;

namespace Leeax.Web.Components.Modals
{
    public class DefaultToastModel : IToastModel
    {
        public const int DefaultDisplayTime = 5000;

        public event Action? Closed;

        public DefaultToastModel()
            : this(null, DefaultDisplayTime, ToastIcon.None, null, false)
        {
        }

        public DefaultToastModel(string? text, int displayTime, ToastIcon icon, IEnumerable<ToastButton>? buttons, bool stacked)
        {
            Icon = icon;
            Text = text;
            Stacked = stacked;
            DisplayTime = displayTime;
            Buttons = buttons?.ToArray() ?? Array.Empty<ToastButton>();
        }

        internal ToastContext GetToastContext()
            => new ToastContext(() => Closed?.Invoke());

        public ToastIcon Icon { get; set; }
        
        public string? Text { get; set; }

        public bool Stacked { get; set; }

        public int DisplayTime { get; set; }

        public ToastButton[] Buttons { get; set; }
    }
}