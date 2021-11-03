using System.Linq;
using System.Collections.Generic;
using System;

namespace Leeax.Web.Components.Modals
{
    public class DefaultToastModel
    {
        public DefaultToastModel()
            : this(null, ToastIcon.None, null, false)
        {
        }

        public DefaultToastModel(string? text, ToastIcon icon, IEnumerable<ToastButton>? buttons, bool stacked)
        {
            Icon = icon;
            Text = text;
            Stacked = stacked;
            Buttons = buttons?.ToArray() ?? Array.Empty<ToastButton>();
        }

        public ToastIcon Icon { get; set; }
        
        public string? Text { get; set; }

        public bool Stacked { get; set; }

        public ToastButton[] Buttons { get; set; }
    }
}