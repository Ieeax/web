using System;

namespace Leeax.Web.Components.Modals
{
    public class ToastButton
    {
        public ToastButton(string? text, Action<ToastContext>? command)
        {
            Text = text;
            Command = command;
        }

        public string? Text { get; set; }

        public Action<ToastContext>? Command { get; set; }
    }
}