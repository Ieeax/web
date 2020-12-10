using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Window
{
    public static class EventManagerExtensions
    {
        #region Mouse events
        public static void AddClickHandler(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.AddEventHandler("click", handler);
                      
        public static void RemoveClickHandler(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.RemoveEventHandler("click", handler);
                      
        public static void AddMouseupHandler(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.AddEventHandler("mouseup", handler);
                      
        public static void RemoveMouseupHandler(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.RemoveEventHandler("mouseup", handler);
                      
        public static void AddMousedownHandler(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.AddEventHandler("mousedown", handler);
                      
        public static void RemoveMousedownHandler(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.RemoveEventHandler("mousedown", handler);
                      
        public static void AddMousemoveHandler(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.AddEventHandler("mousemove", handler);
                      
        public static void RemoveMousemoveHandler(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.RemoveEventHandler("mousemove", handler);

        public static Task AddClickHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.AddEventHandlerAsync("click", handler);

        public static Task RemoveClickHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.RemoveEventHandlerAsync("click", handler);

        public static Task AddMouseupHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.AddEventHandlerAsync("mouseup", handler);

        public static Task RemoveMouseupHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.RemoveEventHandlerAsync("mouseup", handler);

        public static Task AddMousedownHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.AddEventHandlerAsync("mousedown", handler);

        public static Task RemoveMousedownHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.RemoveEventHandlerAsync("mousedown", handler);

        public static Task AddMousemoveHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.AddEventHandlerAsync("mousemove", handler);

        public static Task RemoveMousemoveHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) => manager.RemoveEventHandlerAsync("mousemove", handler);
        #endregion

        #region Touch events
        public static void AddTouchstartHandler(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.AddEventHandler("touchstart", handler);
                      
        public static void RemoveTouchstartHandler(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.RemoveEventHandler("touchstart", handler);
                      
        public static void AddTouchendHandler(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.AddEventHandler("touchend", handler);
                      
        public static void RemoveTouchendHandler(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.RemoveEventHandler("touchend", handler);
                      
        public static void AddTouchcancelHandler(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.AddEventHandler("touchcancel", handler);
                      
        public static void RemoveTouchcancelHandler(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.RemoveEventHandler("touchcancel", handler);
                      
        public static void AddTouchmoveHandler(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.AddEventHandler("touchmove", handler);
                      
        public static void RemoveTouchmoveHandler(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.RemoveEventHandler("touchmove", handler);

        public static Task AddTouchstartHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.AddEventHandlerAsync("touchstart", handler);

        public static Task RemoveTouchstartHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.RemoveEventHandlerAsync("touchstart", handler);

        public static Task AddTouchendHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.AddEventHandlerAsync("touchend", handler);

        public static Task RemoveTouchendHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.RemoveEventHandlerAsync("touchend", handler);

        public static Task AddTouchcancelHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.AddEventHandlerAsync("touchcancel", handler);

        public static Task RemoveTouchcancelHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.RemoveEventHandlerAsync("touchcancel", handler);

        public static Task AddTouchmoveHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.AddEventHandlerAsync("touchmove", handler);

        public static Task RemoveTouchmoveHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) => manager.RemoveEventHandlerAsync("touchmove", handler);
        #endregion

        #region Window events
        public static void AddResizeHandler(this IEventManager manager, Action<EventArgs?> handler) => manager.AddEventHandler("resize", handler);

        public static void RemoveResizeHandler(this IEventManager manager, Action<EventArgs?> handler) => manager.RemoveEventHandler("resize", handler);

        public static Task AddResizeHandlerAsync(this IEventManager manager, Action<EventArgs?> handler) => manager.AddEventHandlerAsync("resize", handler);

        public static Task RemoveResizeHandlerAsync(this IEventManager manager, Action<EventArgs?> handler) => manager.RemoveEventHandlerAsync("resize", handler);
        #endregion
    }
}