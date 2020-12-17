using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Abstractions
{
    public static class JSRuntimeExtensions
    {
        /// <summary>
        /// Imports the file with the given <paramref name="path"/> and stores the reference with the given <paramref name="name"/> in the <paramref name="store"/>.
        /// When no <paramref name="store"/> is supplied the imported reference won't be stored.
        /// If the <paramref name="store"/> already contains a reference with the same <paramref name="name"/>, an exception is thrown.
        /// <para>
        /// The type of the reference gets determined automatically by looking at the supplied <see cref="IJSRuntime"/>.
        /// </para>
        /// </summary>
        /// <param name="jsRuntime">The js-runtime to use for importing.</param>
        /// <param name="path">The path of the file to import.</param>
        /// <param name="name">The name used for storing the imported reference.</param>
        /// <param name="store">The store in which the imported reference gets stored.</param>
        public static async Task<IJSObjectReference> ImportModuleAsync(this IJSRuntime jsRuntime, string path, string? name = null, IJSObjectReferenceStore? store = null)
        {
            return jsRuntime is IJSInProcessRuntime
                ? await ImportModuleAsync<IJSInProcessObjectReference>(jsRuntime, path, name, store)
                : await ImportModuleAsync<IJSObjectReference>(jsRuntime, path, name, store);
        }

        /// <summary>
        /// Imports the file with the given <paramref name="path"/> and stores the reference with the given <paramref name="name"/> in the <paramref name="store"/>.
        /// When no <paramref name="store"/> is supplied the imported reference won't be stored.
        /// If the <paramref name="store"/> already contains a reference with the same <paramref name="name"/>, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type of the reference as which the file gets imported.</typeparam>
        /// <param name="jsRuntime">The js-runtime to use for importing.</param>
        /// <param name="path">The path of the file to import.</param>
        /// <param name="name">The name used for storing the imported reference.</param>
        /// <param name="store">The store in which the imported reference gets stored.</param>
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

        /// <summary>
        /// Imports the file with the given <paramref name="path"/> and stores the reference with the given <paramref name="name"/> in the <paramref name="store"/>.
        /// When a reference with the same <paramref name="name"/> already exists, that reference will be returned.
        /// <para>
        /// The type of the reference gets determined automatically by looking at the supplied <see cref="IJSRuntime"/>.
        /// </para>
        /// </summary>
        /// <param name="jsRuntime">The js-runtime to use for importing.</param>
        /// <param name="path">The path of the file to import.</param>
        /// <param name="name">The name used for storing/retrieving the imported reference.</param>
        /// <param name="store">The store in/from which the imported reference gets stored/retrieved.</param>
        public static async ValueTask<IJSObjectReference> ImportOrGetModuleAsync(this IJSRuntime jsRuntime, string path, string name, IJSObjectReferenceStore store)
        {
            return jsRuntime is IJSInProcessRuntime
                ? await ImportOrGetModuleAsync<IJSInProcessObjectReference>(jsRuntime, path, name, store)
                : await ImportOrGetModuleAsync<IJSObjectReference>(jsRuntime, path, name, store);
        }

        /// <summary>
        /// Imports the file with the given <paramref name="path"/> and stores the reference with the given <paramref name="name"/> in the <paramref name="store"/>.
        /// When a reference with the same <paramref name="name"/> already exists, that reference will be returned.
        /// </summary>
        /// <param name="jsRuntime">The js-runtime to use for importing.</param>
        /// <param name="path">The path of the file to import.</param>
        /// <param name="name">The name used for storing/retrieving the imported reference.</param>
        /// <param name="store">The store in/from which the imported reference gets stored/retrieved.</param>
        public static async ValueTask<T> ImportOrGetModuleAsync<T>(this IJSRuntime jsRuntime, string path, string name, IJSObjectReferenceStore store) 
            where T : IJSObjectReference
        {
            _ = path ?? throw new ArgumentNullException(nameof(path));
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _ = store ?? throw new ArgumentNullException(nameof(store));

            if (store.TryGet(name, out var reference))
            {
                if (reference is not T castedReference)
                {
                    throw new ApplicationException($"An module with the name \"{name}\" was already imported as another type \"{reference?.GetType().FullName}\".");
                }

                return castedReference;
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