using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Leeax.Web.Components.Abstractions
{
    public class JSObjectReferenceStore : IJSObjectReferenceStore
    {
        private readonly Dictionary<string, IJSObjectReference> _references;

        public JSObjectReferenceStore()
        {
            _references = new Dictionary<string, IJSObjectReference>();
        }

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

        public bool TryGet(string name, [MaybeNullWhen(false)] out IJSObjectReference reference)
            => _references.TryGetValue(name, out reference);

        public T Get<T>(string name) where T : IJSObjectReference
            => TryGet<T>(name, out var reference) ? reference! : throw new ApplicationException($"Reference with name \"{name}\" does not exist or cannot be casted to type \"{typeof(T).FullName}\".");

        public bool TryGet<T>(string name, [MaybeNullWhen(false)] out T reference)
            where T : IJSObjectReference?
        {
            if (!_references.TryGetValue(name, out var uncastedReference))
            {
                reference = default!;
                return false;
            }

            if (uncastedReference is not T castedReference)
            {
                reference = default!;
                return false;
            }

            reference = castedReference;
            return true;
        }

        public bool Remove(string name)
            => _references.Remove(name);
    }
}