using Microsoft.JSInterop;
using System.Threading.Tasks;
using Leeax.Web.Components.Abstractions;

namespace Leeax.Web.Components.Clipboard
{
    public class Clipboard : IClipboard
    {
        public static ModuleInfo Module = new ModuleInfo(ModulePath, ModuleKey);
        public const string ModuleKey = "__" + nameof(Clipboard);
        public const string ModulePath = "./_content/Leeax.Web.Components.Clipboard/Clipboard.min.js";

        private readonly IJSInProcessObjectReference? _jsInProcessObjectRef;
        private readonly IJSRuntime _jsRuntime;
        private readonly IJSObjectReferenceStore _jsRefStore;

        public Clipboard(IJSRuntime jsRuntime, IJSObjectReferenceStore jsRefStore)
        {
            _jsRuntime = jsRuntime;
            _jsRefStore = jsRefStore;

            jsRefStore.TryGet(ModuleKey, out _jsInProcessObjectRef);
        }

        /// <inheritdoc/>
        public async ValueTask<bool> WriteAsync(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            return await module!.InvokeAsync<bool>(
                "writeText", 
                value);
        }

        /// <inheritdoc/>
        public async ValueTask<string?> ReadAsync()
        {
            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            return await module!.InvokeAsync<string?>("readText");
        }
    }
}