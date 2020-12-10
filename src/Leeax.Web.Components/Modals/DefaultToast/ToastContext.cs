using Leeax.Web.Internal;
using System;

namespace Leeax.Web.Components.Modals
{
    public class ToastContext
    {
        private readonly Action _closeAction;

        public ToastContext(Action closeAction)
        {
            closeAction.ThrowIfNull();

            _closeAction = closeAction;
        }

        public void Close()
        {
            _closeAction.Invoke();
        }
    }
}