using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace Leeax.Web.Components.Abstractions
{
    public interface IJSObjectReferenceStore
    {
        void Add(string name, IJSObjectReference reference);

        bool TryAdd(string name, IJSObjectReference reference);

        IJSObjectReference Get(string name);

        bool TryGet(string name, [MaybeNullWhen(false)] out IJSObjectReference reference);

        T Get<T>(string name) where T : IJSObjectReference;

        bool TryGet<T>(string name, [MaybeNullWhen(false)] out T reference) where T : IJSObjectReference?;

        bool Remove(string name);
    }
}