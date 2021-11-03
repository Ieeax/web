using Leeax.Web.Components.Presentation;
using Leeax.Web.Internal;
using System;
using System.Threading;

namespace Leeax.Web.Components.Modals
{
    public class ToastState : IDisposable
    {
        private Timer? _timer;
        private bool _timerStarted;

        public event Action<ToastState>? Closed;
        public event Action? StateChanged;

        public ToastState(Type componentType, object model, int displayTime = Timeout.Infinite)
        {
            componentType.ThrowIfNull();
            model.ThrowIfNull();

            IsActive = true;
            ComponentType = componentType;
            Model = model;
            DisplayTime = displayTime < 0 ? Timeout.Infinite : displayTime;

            if (Model is INotifyClosed notifyClosed)
            {
                notifyClosed.Closed += Close;
            }
        }

        public void Close()
        {
            IsActive = false;
            StateChanged?.Invoke();

            _timer?.Dispose();
        }

        internal void TransitionStateChanged(TransitionState state)
        {
            if (!_timerStarted)
            {
                // Start timer except the timeout equals "-1" (infinite, user have to close it manually)
                if (DisplayTime != Timeout.Infinite)
                {
                    _timer = new Timer(
                        _ => Close(),
                        null,
                        DisplayTime,
                        Timeout.Infinite);
                }

                _timerStarted = true;
            }
            
            if (state == TransitionState.Left)
            {
                Closed?.Invoke(this);
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
            
            if (Model is INotifyClosed notifyClosed)
            {
                notifyClosed.Closed -= Close;
            }
        }

        // Required for transition
        public bool IsActive { get; private set; }

        /// <summary>
        /// Gets the associated model.
        /// </summary>
        public object Model { get; }

        /// <summary>
        /// Gets the type of the associated component.
        /// </summary>
        public Type ComponentType { get; }
        
        /// <summary>
        /// Gets the display time in milliseconds.
        /// </summary>
        public int DisplayTime { get; }
    }
}