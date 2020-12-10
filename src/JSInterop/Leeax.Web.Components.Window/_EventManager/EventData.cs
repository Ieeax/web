using System;

namespace Leeax.Web.Components.Window
{
    internal readonly struct EventData
    {
        public EventData(Type argType, Action<EventArgs?> handler)
        {
            ArgumentType = argType ?? throw new ArgumentNullException(nameof(argType));
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public Type ArgumentType { get; }

        public Action<EventArgs?> Handler { get; }
    }
}