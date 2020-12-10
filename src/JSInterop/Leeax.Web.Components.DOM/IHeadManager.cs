using System.Threading.Tasks;

namespace Leeax.Web.Components.DOM
{
    public interface IHeadManager
    {
        void SetTitle(string? value);

        ValueTask SetTitleAsync(string? value);

        void ApplyStyle(string? content);

        void ApplyStyle(string? content, string? type);

        void ApplyStyle(string? content, string? type, string? key);

        ValueTask ApplyStyleAsync(string? content);

        ValueTask ApplyStyleAsync(string? content, string? type);

        ValueTask ApplyStyleAsync(string? content, string? type, string? key);

        bool RemoveStyle(string key);

        ValueTask<bool> RemoveStyleAsync(string key);

        void ApplyLink(string? href, string? rel);

        void ApplyLink(string? href, string? rel, string? key);

        void ApplyLink(LinkTagOptions options);

        ValueTask ApplyLinkAsync(string? href, string? rel);

        ValueTask ApplyLinkAsync(string? href, string? rel, string? key);

        ValueTask ApplyLinkAsync(LinkTagOptions options);

        bool RemoveLink(string key);

        ValueTask<bool> RemoveLinkAsync(string key);

        ValueTask ApplyScriptAsync(string? src);

        ValueTask ApplyScriptAsync(string? src, string? key);

        ValueTask ApplyScriptAsync(ScriptTagOptions options);

        bool RemoveScript(string key);

        ValueTask<bool> RemoveScriptAsync(string key);
    }
}