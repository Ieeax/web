using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Modals
{
    public interface IModalService
    {
        /// <summary>
        /// Shows the modal which is associated with the given model.
        /// </summary>
        Task ShowAsync<TModel>(TModel model) where TModel : INotifyClosed;

        /// <summary>
        /// Shows the modal which is associated with the given model.
        /// </summary>
        Task ShowAsync<TModel>() where TModel : INotifyClosed;

        /// <summary>
        /// Shows the modal which is associated with the given model.
        /// The model itself will be created trough dependency-injection.
        /// </summary>
        Task ShowAsync<TModel>(Action<TModel>? configure) where TModel : INotifyClosed;
    }
}