using System;

namespace Leeax.Web.Components.Modals
{
    public interface IModalRenderService
    {
        event Action? StateChanged;

        /// <summary>
        /// Gets the active modal.
        /// </summary>
        ModalState? ActiveModal { get; }
    }
}