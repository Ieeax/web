using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Window
{
    public interface IEventManager
    {
        void AddEventHandler<TArgument>(string eventName, Action<TArgument?> handler) where TArgument : EventArgs;

        Task AddEventHandlerAsync<TArgument>(string eventName, Action<TArgument?> handler) where TArgument : EventArgs;

        void RemoveEventHandler<TArgument>(string eventName, Action<TArgument?> handler) where TArgument : EventArgs;

        Task RemoveEventHandlerAsync<TArgument>(string eventName, Action<TArgument?> handler) where TArgument : EventArgs;
    }
}