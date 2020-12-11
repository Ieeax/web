using Leeax.Web.Components.Presentation;
using Leeax.Web.Internal;
using System;

namespace Leeax.Web.Components.Modals
{
    public class ModalState : IDisposable
    {
        public event Action<ModalState>? Closed;
        public event Action? StateChanged;

        public ModalState(Type componentType, INotifyClosed model)
        {
            componentType.ThrowIfNull();
            model.ThrowIfNull();

            IsActive = true;
            ComponentKey = Guid.NewGuid().ToString("N");
            ComponentType = componentType;
            Model = model;
            Model.Closed += Close;
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
            Model.Closed -= Close;
        }

        // Required for transition
        public bool IsActive { get; private set; }

        public INotifyClosed Model { get; }

        public Type ComponentType { get; }

        // Unique id to preserve state of component
        public string ComponentKey { get; }
    }
}