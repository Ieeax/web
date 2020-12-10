using System;

namespace Leeax.Web.Components.Modals
{
    public interface INotifyClosed
    {
        event Action? Closed;
    }
}