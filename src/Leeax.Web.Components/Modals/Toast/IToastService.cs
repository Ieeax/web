using System;

namespace Leeax.Web.Components.Modals
{
    public interface IToastService
    {
        /// <summary>
        /// Shows the toast which is associated with the given model.
        /// </summary>
        void Show<TModel>() where TModel : IToastModel;

        /// <summary>
        /// Shows the toast which is associated with the given model.
        /// </summary>
        void Show<TModel>(TModel model) where TModel : IToastModel;

        /// <summary>
        /// Shows the toast which is associated with the given model.
        /// The model itself will be created trough dependency-injection.
        /// </summary>
        void Show<TModel>(Action<TModel>? configure) where TModel : IToastModel;

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