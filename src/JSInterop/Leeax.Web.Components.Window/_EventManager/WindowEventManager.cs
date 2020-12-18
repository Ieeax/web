using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Leeax.Web.Components.Abstractions;
using Microsoft.JSInterop;

namespace Leeax.Web.Components.Window
{
    public class WindowEventManager : IEventManager
    {
        private readonly IJSInProcessObjectReference? _jsInProcessObjectRef;
        private readonly IJSRuntime _jsRuntime;
        private readonly IJSObjectReferenceStore _jsRefStore;

        private readonly Dictionary<string, Dictionary<Type, HashSet<EventHandler>>> _eventHandlerMapping;

        public WindowEventManager(IJSRuntime jsRuntime, IJSObjectReferenceStore jsRefStore)
        {
            _jsRuntime = jsRuntime;
            _jsRefStore = jsRefStore;
            _eventHandlerMapping = new();

            jsRefStore.TryGet(WindowService.ModuleKey, out _jsInProcessObjectRef);
        }

        public void AddEventHandler<TArgument>(string eventName, Action<TArgument?> handler) 
            where TArgument : EventArgs
        {
            if (AddEventHandlerInternal(eventName, handler))
            {
                // Add inital event-handler for the given event
                _jsInProcessObjectRef!.InvokeVoid(
                    "addEventHandler",
                    eventName,
                    nameof(HandleEventCallback),
                    DotNetObjectReference.Create(this));
            }
        }

        public async Task AddEventHandlerAsync<TArgument>(string eventName, Action<TArgument?> handler)
            where TArgument : EventArgs
        {
            var module = await _jsRuntime
                .ImportOrGetModuleAsync(WindowService.ModulePath, WindowService.ModuleKey, _jsRefStore);

            if (AddEventHandlerInternal(eventName, handler))
            {
                // Add inital event-handler for the given event
                await module.InvokeVoidAsync(
                    "addEventHandler",
                    eventName,
                    nameof(HandleEventCallback),
                    DotNetObjectReference.Create(this));
            }
        }

        private bool AddEventHandlerInternal<TArgument>(string eventName, Action<TArgument?> handler)
            where TArgument : EventArgs
        {
            _ = eventName ?? throw new ArgumentNullException(nameof(eventName));
            _ = handler ?? throw new ArgumentNullException(nameof(handler));

            // Create the event-handler, encapsulated for easy invocation
            var eventHandler = new EventHandler(handler, e => handler(e as TArgument));

            if (!_eventHandlerMapping.TryGetValue(eventName, out var eventArgsCallbackMapping))
            {
                eventArgsCallbackMapping = new();
                _eventHandlerMapping.Add(eventName, eventArgsCallbackMapping);
            }
            else
            {
                if (eventArgsCallbackMapping.TryGetValue(typeof(TArgument), out var handlers))
                {
                    if (!handlers.Add(eventHandler))
                    {
                        throw new ArgumentException("Passed event-handler was already registered. An event-handler can only be registered once per event.", nameof(handler));
                    }

                    return false;
                }
            }

            eventArgsCallbackMapping.Add(
                typeof(TArgument),
                new HashSet<EventHandler>()
                {
                    eventHandler
                });

            return true;
        }

        public void RemoveEventHandler<TArgument>(string eventName, Action<TArgument?> handler) 
            where TArgument : EventArgs
        {
            if (RemoveEventHandlerInternal(eventName, handler))
            {
                // Remove event-handler when the last event-handler was removed
                _jsInProcessObjectRef!.InvokeVoid("removeEventHandler");
            }
        }

        public async Task RemoveEventHandlerAsync<TArgument>(string eventName, Action<TArgument?> handler)
            where TArgument : EventArgs
        {
            var module = await _jsRuntime
                .ImportOrGetModuleAsync(WindowService.ModulePath, WindowService.ModuleKey, _jsRefStore);

            if (RemoveEventHandlerInternal(eventName, handler))
            {
                // Remove event-handler when the last event-handler was removed
                await module.InvokeVoidAsync("removeEventHandler");
            }
        }

        public bool RemoveEventHandlerInternal<TArgument>(string eventName, Action<TArgument?> handler)
            where TArgument : EventArgs
        {
            _ = eventName ?? throw new ArgumentNullException(nameof(eventName));
            _ = handler ?? throw new ArgumentNullException(nameof(handler));

            if (!_eventHandlerMapping.TryGetValue(eventName, out var eventArgsCallbackMapping))
            {
                return false;
            }

            if (!eventArgsCallbackMapping.TryGetValue(typeof(TArgument), out var handlers))
            {
                return false;
            }

            if (!handlers.Remove(new EventHandler(handler, null)))
            {
                return false;
            }

            // Check whether we can remove the current listener from javascript too
            if (handlers.Count == 0)
            {
                eventArgsCallbackMapping.Remove(typeof(TArgument));
            }

            if (eventArgsCallbackMapping.Count == 0)
            {
                _eventHandlerMapping.Remove(eventName);

                // No more handlers for this event registered
                // -> We can safely remove the listener from js
                return true;
            }

            return false;
        }

        [JSInvokable]
        public void HandleEventCallback(string eventName, string argsAsJson)
        {
            if (eventName == null)
            {
                Console.WriteLine($"[WARN] Invalid event-name was passed from javascript.");
                return;
            }

            if (!_eventHandlerMapping.TryGetValue(eventName, out var eventArgsCallbackMapping))
            {
                Console.WriteLine($"[WARN] Invalid event-name \"{eventName}\" was passed from javascript. No event-handlers registered.");
                return;
            }

            foreach (var curItem in eventArgsCallbackMapping)
            {
                EventArgs? parsedArgs = null;

                if (curItem.Key == typeof(EventArgs))
                {
                    // Ignore deserializing if user specified type "EventArgs"
                    // -> User doesn't care for any arguments
                    parsedArgs = EventArgs.Empty;
                }
                else if (string.IsNullOrEmpty(argsAsJson))
                {
                    Console.WriteLine($"[WARN] Passed argument-string from javascript cannot be deserialized. Skipping {curItem.Value.Count} event-handlers ...");
                    continue;
                }
                else
                {
                    try
                    {
                        parsedArgs = JsonSerializer.Deserialize(argsAsJson, curItem.Key) as EventArgs;
                    }
                    catch (JsonException)
                    {
                        Console.WriteLine($"[WARN] Arguments for event \"{eventName}\" couldn't be deserialized to .NET type \"{curItem.Key.FullName}\". Skipping {curItem.Value.Count} event-handlers ...");
                        continue;
                    }
                }
                
                // Invoke all callbacks/event-handler
                foreach (var curHandler in curItem.Value)
                {
                    try
                    {
                        curHandler.Invoke(parsedArgs);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"[WARN] An exception occured during executing an event-handler for event \"{eventName}\".");
                    }
                }
            }
        }
    }
}