using Leeax.Web.Components.Presentation;
using Leeax.Web.Internal;
using System;

namespace Leeax.Web.Components.Modals
{
    public class ModalState : IDisposable
    {
        public event Action<ModalState>? Closed;
        public event Action? StateChanged;

        public ModalState(Type componentType, object model)
        {
            componentType.ThrowIfNull();
            model.ThrowIfNull();

            IsActive = true;
            ComponentType = componentType;
            Model = model;

            if (Model is INotifyClosed notifyClosed)
            {
                notifyClosed.Closed += Close;
            }
        }

        internal void TransitionStateChanged(TransitionState state)
        {
            if (state == TransitionState.Left)
            {
                Closed?.Invoke(this);
            }
        }

        public void Close()
        {
            IsActive = false;
            StateChanged?.Invoke();
        }

        public void Dispose()
        {
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
    }
}