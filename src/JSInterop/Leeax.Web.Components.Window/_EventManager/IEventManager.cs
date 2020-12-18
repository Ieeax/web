using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Window
{
    public interface IEventManager
    {
        /// <summary>
        /// Adds the passed <paramref name="handler"/> for the event with the given <paramref name="eventName"/>.
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <param name="eventName">The name of the event for which the handler should be added.</param>
        /// <param name="handler">The handler to invoke whenever the event is raised.</param>
        void AddEventHandler<TArgument>(string eventName, Action<TArgument?> handler) where TArgument : EventArgs;

        /// <summary>
        /// Adds the passed <paramref name="handler"/> for the event with the given <paramref name="eventName"/>.
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <param name="eventName">The name of the event for which the handler should be added.</param>
        /// <param name="handler">The handler to invoke whenever the event is raised.</param>
        Task AddEventHandlerAsync<TArgument>(string eventName, Action<TArgument?> handler) where TArgument : EventArgs;

        /// <summary>
        /// Removes the passed <paramref name="handler"/> from the event with the given <paramref name="eventName"/>.
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <param name="eventName">The name of the event for which the handler should be removed.</param>
        /// <param name="handler">The handler to remove.</param>
        void RemoveEventHandler<TArgument>(string eventName, Action<TArgument?> handler) where TArgument : EventArgs;

        /// <summary>
        /// Removes the passed <paramref name="handler"/> from the event with the given <paramref name="eventName"/>.
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <param name="eventName">The name of the event for which the handler should be removed.</param>
        /// <param name="handler">The handler to remove.</param>
        Task RemoveEventHandlerAsync<TArgument>(string eventName, Action<TArgument?> handler) where TArgument : EventArgs;
    }
}