using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Window
{
    public static class EventManagerExtensions
    {
        #region Mouse events
        public static void AddClickHandler(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.AddEventHandler(EventNames.Click, handler);
                      
        public static void RemoveClickHandler(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.RemoveEventHandler(EventNames.Click, handler);
                      
        public static void AddMouseupHandler(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.AddEventHandler(EventNames.MouseUp, handler);
                      
        public static void RemoveMouseupHandler(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.RemoveEventHandler(EventNames.MouseUp, handler);
                      
        public static void AddMousedownHandler(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.AddEventHandler(EventNames.MouseDown, handler);
                      
        public static void RemoveMousedownHandler(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.RemoveEventHandler(EventNames.MouseDown, handler);
                      
        public static void AddMousemoveHandler(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.AddEventHandler(EventNames.MouseMove, handler);
                      
        public static void RemoveMousemoveHandler(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.RemoveEventHandler(EventNames.MouseMove, handler);

        public static Task AddClickHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.AddEventHandlerAsync(EventNames.Click, handler);

        public static Task RemoveClickHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.RemoveEventHandlerAsync(EventNames.Click, handler);

        public static Task AddMouseupHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.AddEventHandlerAsync(EventNames.MouseUp, handler);

        public static Task RemoveMouseupHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.RemoveEventHandlerAsync(EventNames.MouseUp, handler);

        public static Task AddMousedownHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.AddEventHandlerAsync(EventNames.MouseDown, handler);

        public static Task RemoveMousedownHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.RemoveEventHandlerAsync(EventNames.MouseDown, handler);

        public static Task AddMousemoveHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.AddEventHandlerAsync(EventNames.MouseMove, handler);

        public static Task RemoveMousemoveHandlerAsync(this IEventManager manager, Action<MouseEventArgs?> handler) 
            => manager.RemoveEventHandlerAsync(EventNames.MouseMove, handler);
        #endregion

        #region Touch events
        public static void AddTouchstartHandler(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.AddEventHandler(EventNames.TouchStart, handler);
                      
        public static void RemoveTouchstartHandler(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.RemoveEventHandler(EventNames.TouchStart, handler);
                      
        public static void AddTouchendHandler(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.AddEventHandler(EventNames.TouchEnd, handler);
                      
        public static void RemoveTouchendHandler(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.RemoveEventHandler(EventNames.TouchEnd, handler);
                      
        public static void AddTouchcancelHandler(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.AddEventHandler(EventNames.TouchCancel, handler);
                      
        public static void RemoveTouchcancelHandler(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.RemoveEventHandler(EventNames.TouchCancel, handler);
                      
        public static void AddTouchmoveHandler(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.AddEventHandler(EventNames.TouchMove, handler);
                      
        public static void RemoveTouchmoveHandler(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.RemoveEventHandler(EventNames.TouchMove, handler);

        public static Task AddTouchstartHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.AddEventHandlerAsync(EventNames.TouchStart, handler);

        public static Task RemoveTouchstartHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.RemoveEventHandlerAsync(EventNames.TouchStart, handler);

        public static Task AddTouchendHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.AddEventHandlerAsync(EventNames.TouchEnd, handler);

        public static Task RemoveTouchendHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.RemoveEventHandlerAsync(EventNames.TouchEnd, handler);

        public static Task AddTouchcancelHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.AddEventHandlerAsync(EventNames.TouchCancel, handler);

        public static Task RemoveTouchcancelHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.RemoveEventHandlerAsync(EventNames.TouchCancel, handler);

        public static Task AddTouchmoveHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.AddEventHandlerAsync(EventNames.TouchMove, handler);

        public static Task RemoveTouchmoveHandlerAsync(this IEventManager manager, Action<TouchEventArgs?> handler) 
            => manager.RemoveEventHandlerAsync(EventNames.TouchMove, handler);
        #endregion
    }
}