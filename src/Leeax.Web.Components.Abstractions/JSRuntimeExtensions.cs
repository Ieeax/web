using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Abstractions
{
    public static class JSRuntimeExtensions
    {
        public static Task<IJSObjectReference> ImportModuleAsync(this IJSRuntime jsRuntime, string path, string? name = null, IJSObjectReferenceStore? store = null)
            => ImportModuleAsync<IJSObjectReference>(jsRuntime, path, name, store);

        public static async Task<T> ImportModuleAsync<T>(this IJSRuntime jsRuntime, string path, string? name = null, IJSObjectReferenceStore? store = null) 
            where T : IJSObjectReference
        {
            _ = path ?? throw new ArgumentNullException(nameof(path));

            if (store != null
                && name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (store != null
                && store.TryGet(name!, out _))
            {
                throw new ApplicationException($"Module with name \"{name}\" was already imported in the supplied store.");
            }

            // Import module from javascript
            var jsObjectRef = await jsRuntime.InvokeAsync<T>("import", path);

            if (jsObjectRef == null)
            {
                throw new ApplicationException($"Module with name \"{name}\" couldn't be imported.");
            }

            // Add module to store
            store?.Add(name!, jsObjectRef);

            return jsObjectRef;
        }

        public static ValueTask<IJSObjectReference> ImportOrGetModuleAsync(this IJSRuntime jsRuntime, string path, string name, IJSObjectReferenceStore store)
            => ImportOrGetModuleAsync<IJSObjectReference>(jsRuntime, path, name, store);

        public static async ValueTask<T> ImportOrGetModuleAsync<T>(this IJSRuntime jsRuntime, string path, string name, IJSObjectReferenceStore store) 
            where T : IJSObjectReference
        {
            _ = path ?? throw new ArgumentNullException(nameof(path));
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _ = store ?? throw new ArgumentNullException(nameof(store));

            if (store.TryGet<T>(name, out var reference))
            {
                return reference;
            }

            // Import module from javascript
            var jsObjectRef = await jsRuntime.InvokeAsync<T>("import", path);

            if (jsObjectRef == null)
            {
                throw new ApplicationException($"Module with name \"{name}\" couldn't be imported.");
            }

            // Add module to store
            store.Add(name, jsObjectRef);

            return jsObjectRef;
        }
    }
}