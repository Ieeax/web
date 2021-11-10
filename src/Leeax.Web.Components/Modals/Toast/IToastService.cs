using System;
using System.Threading;

namespace Leeax.Web.Components.Modals
{
    public interface IToastService
    {
        /// <summary>
        /// Shows the toast which is associated with the given model.
        /// </summary>
        void Show<TModel>(int displayTime = 5000);

        /// <summary>
        /// Shows the toast which is associated with the given model.
        /// </summary>
        void Show<TModel>(TModel model, int displayTime = 5000);

        /// <summary>
        /// Shows the toast which is associated with the given model.
        /// The model itself will be created trough dependency-injection.
        /// </summary>
        void Show<TModel>(Action<TModel>? configure, int displayTime = 5000);

        /// <summary>
        /// Clears all currently displayed toasts.
        /// </summary>
        void ClearAll();

        /// <summary>
        /// Gets or sets the toast-position.
        /// </summary>
        ToastPosition ToastPosition { get; set; }
    }
}