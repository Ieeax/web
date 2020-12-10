using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Abstractions
{
    public class JsObjectReferenceStore : IJSObjectReferenceStore
    {
        private readonly Dictionary<string, IJSObjectReference> _references;
        private readonly IJSRuntime _jsRuntime;

        public JsObjectReferenceStore(IJSRuntime jsRuntime)
        {
            _references = new Dictionary<string, IJSObjectReference>();
            _jsRuntime = jsRuntime;
        }

        public Task<IJSObjectReference> ImportModuleAsync(string name, string path)
            => ImportModuleAsync<IJSObjectReference>(name, path);

        public async Task<T> ImportModuleAsync<T>(string name, string path) 
            where T : IJSObjectReference
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _ = path ?? throw new ArgumentNullException(nameof(path));

            if (_references.ContainsKey(name))
            {
                throw new ApplicationException($"Module with name \"{name}\" was already imported.");
            }

            // Import module from javascript
            var jsObjectRef = await _jsRuntime.InvokeAsync<T>("import", path);

            if (jsObjectRef == null)
            {
                throw new ApplicationException($"Module with name \"{name}\" couldn't be imported.");
            }

            // Add module to store
            Add(name, jsObjectRef);

            return jsObjectRef;
        }

        public ValueTask<IJSObjectReference> ImportOrGetModuleAsync(string name, string path)
            => ImportOrGetModuleAsync<IJSObjectReference>(name, path);

        public async ValueTask<T> ImportOrGetModuleAsync<T>(string name, string path) 
            where T : IJSObjectReference
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _ = path ?? throw new ArgumentNullException(nameof(path));

            if (TryGet<T>(name, out var reference))
            {
                return reference!;
            }

            // Import module from javascript
            var jsObjectRef = await _jsRuntime.InvokeAsync<T>("import", path);

            if (jsObjectRef == null)
            {
                throw new ApplicationException($"Module with name \"{name}\" couldn't be imported.");
            }

            // Add module to store
            Add(name, jsObjectRef);

            return jsObjectRef;
        }

        // TODO: We may want to check for null values?
        public void Add(string name, IJSObjectReference reference)
        {
            if (!TryAdd(name, reference))
            {
                throw new ApplicationException($"Reference with name \"{name}\" already exists.");
            }
        }

        public bool TryAdd(string name, IJSObjectReference reference)
            => _references.TryAdd(name, reference);

        public IJSObjectReference Get(string name)
            => TryGet(name, out var reference) ? reference! : throw new ApplicationException($"Reference with name \"{name}\" does not exist."); 

        public bool TryGet(string name, out IJSObjectReference? reference)
            => _references.TryGetValue(name, out reference);

        public T Get<T>(string name) where T : IJSObjectReference
            => TryGet<T>(name, out var reference) ? reference! : throw new ApplicationException($"Reference with name \"{name}\" does not exist or cannot be casted to type \"{typeof(T).FullName}\".");

        public bool TryGet<T>(string name, out T? reference) 
            where T : IJSObjectReference
        {
            if (!_references.TryGetValue(name, out var uncastedReference))
            {
                reference = default;
                return false;
            }

            if (uncastedReference is not T castedReference)
            {
                reference = default;
                return false;
            }

            reference = castedReference;
            return true;
        }

        public bool Remove(string name)
            => _references.Remove(name);
    }
}