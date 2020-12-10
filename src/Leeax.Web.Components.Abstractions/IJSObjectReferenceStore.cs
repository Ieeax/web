using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Abstractions
{
    public interface IJSObjectReferenceStore
    {
        Task<IJSObjectReference> ImportModuleAsync(string name, string path);

        Task<T> ImportModuleAsync<T>(string name, string path) where T : IJSObjectReference;

        ValueTask<IJSObjectReference> ImportOrGetModuleAsync(string name, string path);

        ValueTask<T> ImportOrGetModuleAsync<T>(string name, string path) where T : IJSObjectReference;

        void Add(string name, IJSObjectReference reference);

        bool TryAdd(string name, IJSObjectReference reference);

        IJSObjectReference Get(string name);

        bool TryGet(string name, out IJSObjectReference? reference);

        T Get<T>(string name) where T : IJSObjectReference;

        bool TryGet<T>(string name, out T? reference) where T : IJSObjectReference;

        bool Remove(string name);
    }
}