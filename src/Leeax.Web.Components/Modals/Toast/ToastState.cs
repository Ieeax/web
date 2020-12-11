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

        public ToastState(Type componentType, IToastModel model)
        {
            componentType.ThrowIfNull();
            model.ThrowIfNull();

            IsActive = true;
            ComponentKey = Guid.NewGuid().ToString("N");
            ComponentType = componentType;
            Model = model;
            Model.Closed += Close;
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
                if (Model.DisplayTime != Timeout.Infinite)
                {
                    _timer = new Timer(
                        x => Close(),
                        null,
                        Model.DisplayTime,
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
            Model.Closed -= Close;
        }

        // Required for transition
        public bool IsActive { get; private set; }

        public IToastModel Model { get; }

        public Type ComponentType { get; }

        // Unique id to preserve state of component
        public string ComponentKey { get; }
    }
}