using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Leeax.Web.Components.Window
{
    public class EventManager : IEventManager
    {
        private readonly IJSInProcessObjectReference? _jsInProcessObjectRef;
        private readonly IJSObjectReference _jsObjectRef;

        private readonly Dictionary<Guid, EventData> _subscriptionIdHandlerMapping;
        private readonly Dictionary<EventInfo, Guid> _handlerSubscriptionIdMapping;

        public EventManager(IJSObjectReference jsObjectRef)
        {
            _jsObjectRef = jsObjectRef;
            _jsInProcessObjectRef = jsObjectRef as IJSInProcessObjectReference;

            _subscriptionIdHandlerMapping = new Dictionary<Guid, EventData>();
            _handlerSubscriptionIdMapping = new Dictionary<EventInfo, Guid>();
        }

        public void AddEventHandler<TArgument>(string eventName, Action<TArgument?> handler) 
            where TArgument : EventArgs
        {
            var subscriptionId = AddEventHandlerInternal(eventName, handler);

            // Trigger js function to add a eventhandler
            _jsInProcessObjectRef!.InvokeVoid(
                "addEventHandler",
                eventName,
                subscriptionId,
                nameof(HandleEventCallback),
                DotNetObjectReference.Create(this));
        }

        public async Task AddEventHandlerAsync<TArgument>(string eventName, Action<TArgument?> handler)
            where TArgument : EventArgs
        {
            var subscriptionId = AddEventHandlerInternal(eventName, handler);

            // Trigger js function to add a eventhandler
            await _jsObjectRef.InvokeVoidAsync(
                "addEventHandler",
                eventName,
                subscriptionId,
                nameof(HandleEventCallback),
                DotNetObjectReference.Create(this));
        }

        private Guid AddEventHandlerInternal<TArgument>(string eventName, Action<TArgument?> handler)
            where TArgument : EventArgs
        {
            _ = eventName ?? throw new ArgumentNullException(nameof(eventName));
            _ = handler ?? throw new ArgumentNullException(nameof(handler));

            var subscriptionId = Guid.NewGuid();

            _subscriptionIdHandlerMapping.Add(
                subscriptionId,
                new EventData(typeof(TArgument), (e) => handler(e as TArgument)));

            _handlerSubscriptionIdMapping.Add(
                new EventInfo(eventName, handler),
                subscriptionId);

            return subscriptionId;
        }

        public void RemoveEventHandler<TArgument>(string eventName, Action<TArgument?> handler) 
            where TArgument : EventArgs
        {
            var subscriptionId = RemoveEventHandlerInternal(eventName, handler);

            // Trigger js function to remove the eventhandler
            _jsInProcessObjectRef!.InvokeVoid(
                "removeEventHandler",
                subscriptionId);
        }

        public async Task RemoveEventHandlerAsync<TArgument>(string eventName, Action<TArgument?> handler)
            where TArgument : EventArgs
        {
            var subscriptionId = RemoveEventHandlerInternal(eventName, handler);

            // Trigger js function to remove the eventhandler
            await _jsObjectRef.InvokeVoidAsync(
                "removeEventHandler",
                subscriptionId);
        }

        public Guid RemoveEventHandlerInternal<TArgument>(string eventName, Action<TArgument?> handler)
            where TArgument : EventArgs
        {
            _ = eventName ?? throw new ArgumentNullException(nameof(eventName));
            _ = handler ?? throw new ArgumentNullException(nameof(handler));

            var eventInfo = new EventInfo(eventName, handler);

            if (!_handlerSubscriptionIdMapping.TryGetValue(eventInfo, out var subscriptionId))
            {
                throw new ApplicationException($"No matching handler to remove was found. Ensure the handler for '{eventName}' is registered.");
            }

            _subscriptionIdHandlerMapping.Remove(subscriptionId);
            _handlerSubscriptionIdMapping.Remove(eventInfo);

            return subscriptionId;
        }

        [JSInvokable]
        public void HandleEventCallback(Guid sid, string args)
        {
            if (sid == Guid.Empty)
            {
                // If this message gets logged, probably something with jsinterop went wrong
                Console.WriteLine($"[WARN] Invalid subscription-id '{sid}' wered passed from javascript.");
                return;
            }

            if (_subscriptionIdHandlerMapping.TryGetValue(sid, out var eventInfo))
            {
                EventArgs? parsedArgs = null;
                try
                {
                    parsedArgs = args == null
                        ? null
                        : JsonSerializer.Deserialize(args, eventInfo.ArgumentType) as EventArgs;
                }
                catch (JsonException)
                {
                    Console.WriteLine($"[WARN] Arguments for subscription-id '{sid}' couldn't be parsed to .NET-type '{eventInfo.ArgumentType.FullName}'.");
                }

                eventInfo.Handler(parsedArgs);
                return;
            }

            // If this message gets logged, we probably have a problem with unsubscribing from an eventhandler
            Console.WriteLine($"[WARN] Unkown subscription-id '{sid}' were passed from javascript.");
        }
    }
}