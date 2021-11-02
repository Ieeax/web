using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Modals
{
    public interface IModelComponent<TModel>
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        [Parameter]
        public TModel Model { get; set; }
    }
}