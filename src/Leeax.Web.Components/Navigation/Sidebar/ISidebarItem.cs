using System;

namespace Leeax.Web.Components.Navigation
{
    public interface ISidebarItem
    {
        event EventHandler<string?>? ActiveChanged;
    }
}