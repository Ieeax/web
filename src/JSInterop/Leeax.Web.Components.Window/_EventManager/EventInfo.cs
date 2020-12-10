using System;

namespace Leeax.Web.Components.Window
{
    internal readonly struct EventInfo : IEquatable<EventInfo>
    {
        public EventInfo(string eventName, object handler)
        {
            EventName = eventName ?? throw new ArgumentNullException(nameof(eventName));
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public string EventName { get; }

        public object Handler { get; }

        public bool Equals(EventInfo other)
        {
            return (EventName == other.EventName
                && ((Handler == null && other.Handler == null)
                    || (Handler != null && Handler.Equals(other.Handler))));
        }
    }
}