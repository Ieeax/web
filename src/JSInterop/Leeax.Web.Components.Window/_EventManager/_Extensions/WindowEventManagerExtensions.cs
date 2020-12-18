using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Window
{
    public static class WindowEventManagerExtensions
    {
        public static void AddResizeHandler(this IEventManager manager, Action<EventArgs?> handler) 
            => manager.AddEventHandler(EventNames.Resize, handler);

        public static void RemoveResizeHandler(this IEventManager manager, Action<EventArgs?> handler) 
            => manager.RemoveEventHandler(EventNames.Resize, handler);

        public static Task AddResizeHandlerAsync(this IEventManager manager, Action<EventArgs?> handler) 
            => manager.AddEventHandlerAsync(EventNames.Resize, handler);

        public static Task RemoveResizeHandlerAsync(this IEventManager manager, Action<EventArgs?> handler) 
            => manager.RemoveEventHandlerAsync(EventNames.Resize, handler);
    }
}