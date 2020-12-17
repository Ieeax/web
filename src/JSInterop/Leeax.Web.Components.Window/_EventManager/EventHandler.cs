using System;

namespace Leeax.Web.Components.Window
{
    internal readonly struct EventHandler : IEquatable<EventHandler>
    {
        private readonly Action<EventArgs?>? _invokableHandler;

        public EventHandler(object handler, Action<EventArgs?>? invokableHandler)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));

            _invokableHandler = invokableHandler;
        }

        public object Handler { get; }

        public void Invoke(EventArgs? args) => _invokableHandler?.Invoke(args);

        public bool Equals(EventHandler other)
        {
            return (Handler == null && other.Handler == null)
                || (Handler != null && Handler.Equals(other.Handler));
        }

        public override bool Equals(object? obj)
        {
            return obj is EventHandler a && Equals(a);
        }

        public override int GetHashCode()
        {
            return Handler.GetHashCode();
        }
    }
}