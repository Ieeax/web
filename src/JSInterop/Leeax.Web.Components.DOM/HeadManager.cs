using System;
using System.Threading.Tasks;
using Leeax.Web.Components.Abstractions;
using Microsoft.JSInterop;

namespace Leeax.Web.Components.DOM
{
    public class HeadManager : IHeadManager
    {
        public static ModuleInfo Module = new ModuleInfo(ModulePath, ModuleKey);
        public const string ModuleKey = "__" + nameof(HeadManager);
        public const string ModulePath = "./_content/Leeax.Web.Components.DOM/HeadManager.min.js";

        private readonly IJSInProcessObjectReference? _jsInProcessObjectRef;
        private readonly IJSRuntime _jsRuntime;
        private readonly IJSObjectReferenceStore _jsRefStore;

        public HeadManager(IJSRuntime jsRuntime, IJSObjectReferenceStore jsRefStore)
        {
            _jsRuntime = jsRuntime;
            _jsRefStore = jsRefStore;

            jsRefStore.TryGet(ModuleKey, out _jsInProcessObjectRef);
        }

        public void SetTitle(string? value)
        {
            _jsInProcessObjectRef!.InvokeVoid(
                "setTitle", 
                value);
        }

        public async ValueTask SetTitleAsync(string? value)
        {
            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            await module.InvokeVoidAsync(
                "setTitle",
                value);
        }

        public void ApplyStyle(string? content)
            => ApplyStyle(content, null, null);

        public void ApplyStyle(string? content, string? type)
            => ApplyStyle(content, type, null);

        public void ApplyStyle(string? content, string? type, string? key)
        {        
            _jsInProcessObjectRef!.InvokeVoid(
                "addOrUpdateStyle",
                content,
                type,
                key);
        }

        public ValueTask ApplyStyleAsync(string? content)
            => ApplyStyleAsync(content, null, null);

        public ValueTask ApplyStyleAsync(string? content, string? type)
            => ApplyStyleAsync(content, type, null);

        public async ValueTask ApplyStyleAsync(string? content, string? type, string? key)
        {
            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            await module.InvokeVoidAsync(
                "addOrUpdateStyle",
                content,
                type,
                key);
        }

        public bool RemoveStyle(string key)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            return _jsInProcessObjectRef!.Invoke<bool>(
                "removeStyle",
                key);
        }

        public async ValueTask<bool> RemoveStyleAsync(string key)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            return await module.InvokeAsync<bool>(
                "removeStyle",
                key);
        }

        public void ApplyLink(string? href, string? rel)
            => ApplyLink(new LinkTagOptions(href, rel));

        public void ApplyLink(string? href, string? rel, string? key)
            => ApplyLink(new LinkTagOptions(href, rel, key));

        public void ApplyLink(LinkTagOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            _jsInProcessObjectRef!.InvokeVoid(
                "addOrUpdateLink",
                options);
        }

        public ValueTask ApplyLinkAsync(string? href, string? rel)
            => ApplyLinkAsync(new LinkTagOptions(href, rel));

        public ValueTask ApplyLinkAsync(string? href, string? rel, string? key)
            => ApplyLinkAsync(new LinkTagOptions(href, rel, key));

        public async ValueTask ApplyLinkAsync(LinkTagOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            await module.InvokeVoidAsync(
                "addOrUpdateLink",
                options);
        }

        public bool RemoveLink(string key)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            return _jsInProcessObjectRef!.Invoke<bool>(
                "removeLink",
                key);
        }

        public async ValueTask<bool> RemoveLinkAsync(string key)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            return await module.InvokeAsync<bool>(
                "removeLink",
                key);
        }

        public ValueTask ApplyScriptAsync(string? src)
            => ApplyScriptAsync(new ScriptTagOptions(src));

        public ValueTask ApplyScriptAsync(string? src, string? key)
            => ApplyScriptAsync(new ScriptTagOptions(src, -1, key));

        public async ValueTask ApplyScriptAsync(ScriptTagOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            await module.InvokeVoidAsync(
                "addOrUpdateScript",
                options);
        }

        public bool RemoveScript(string key)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            return _jsInProcessObjectRef!.Invoke<bool>(
                "removeScript",
                key);
        }

        public async ValueTask<bool> RemoveScriptAsync(string key)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            return await module.InvokeAsync<bool>(
                "removeScript",
                key);
        }
    }
}