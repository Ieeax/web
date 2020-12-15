using Leeax.Web.Components.Abstractions;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Window
{
    public class WindowService : IWindowService
    {
        public static ModuleInfo Module = new ModuleInfo(ModulePath, ModuleKey);
        public const string ModuleKey = "__" + nameof(WindowService);
        public const string ModulePath = "./_content/Leeax.Web.Components.Window/WindowService.min.js";

        private readonly IJSInProcessObjectReference? _jsInProcessObjectRef;
        private readonly IJSRuntime _jsRuntime;
        private readonly IJSObjectReferenceStore _jsRefStore;
        private IEventManager? _eventManager;

        public WindowService(IJSRuntime jsRuntime, IJSObjectReferenceStore jsRefStore)
        {
            _jsRuntime = jsRuntime;
            _jsRefStore = jsRefStore;

            if (jsRefStore.TryGet(ModuleKey, out _jsInProcessObjectRef))
            {
                _eventManager = new EventManager(_jsInProcessObjectRef!);
            }
        }

        public void Open(string? url, bool openInNewTab)
            => Open(url, openInNewTab ? "_blank" : null);

        public void Open(string? url, string? windowName)
        {
            _jsInProcessObjectRef!.InvokeVoid(
                "open",
                url,
                windowName);
        }

        public ValueTask OpenAsync(string? url, bool openInNewTab)
            => OpenAsync(url, openInNewTab ? "_blank" : null);

        public async ValueTask OpenAsync(string? url, string? windowName)
        {
            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            await module.InvokeVoidAsync(
                "open",
                url,
                windowName);
        }

        public void ScrollTo(int top, bool smooth = false)
            => ScrollTo(top, null, smooth);

        public void ScrollTo(int? top, int? left = null, bool smooth = false)
        {
            _jsInProcessObjectRef!.InvokeVoid(
                "scrollTo",
                new object?[]
                {
                    top,
                    left,
                    smooth
                });
        }

        public ValueTask ScrollToAsync(int top, bool smooth = false)
            => ScrollToAsync(top, null, smooth);

        public async ValueTask ScrollToAsync(int? top, int? left = null, bool smooth = false)
        {
            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            await module.InvokeVoidAsync(
                "scrollTo",
                new object?[]
                {
                    top,
                    left,
                    smooth
                });
        }

        public void ShowAlert(string? message)
        {
            _jsInProcessObjectRef!.InvokeVoid(
                "window.alert",
                message);
        }

        public bool ShowConfirm(string? message)
        {
            return _jsInProcessObjectRef!.Invoke<bool>(
                "window.confirm",
                message);
        }

        public string? ShowPrompt(string? message)
        {
            return _jsInProcessObjectRef!.Invoke<string>(
                "window.prompt",
                message);
        }

        public async ValueTask ShowAlertAsync(string? message)
        {
            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            await module.InvokeVoidAsync(
                "window.alert",
                message);
        }

        public async ValueTask<bool> ShowConfirmAsync(string? message)
        {
            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            return await module.InvokeAsync<bool>(
                "window.confirm",
                message);
        }

        public async ValueTask<string?> ShowPromptAsync(string? message)
        {
            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            return await module.InvokeAsync<string>(
                "window.prompt",
                message);
        }

        public T? GetProperty<T>(string propertyName)
        {
            _ = propertyName ?? throw new ArgumentNullException(nameof(propertyName));

            return _jsInProcessObjectRef!.Invoke<T>(
                "getProperty",
                propertyName);
        }

        public async ValueTask<T?> GetPropertyAsync<T>(string propertyName)
        {
            _ = propertyName ?? throw new ArgumentNullException(nameof(propertyName));

            var module = await _jsRuntime
                .ImportOrGetModuleAsync(ModulePath, ModuleKey, _jsRefStore);

            return await module.InvokeAsync<T>(
                "getProperty",
                propertyName);
        }

        public IEventManager EventManager
        { 
            get => _eventManager ?? throw new NotSupportedException();
            private set => _eventManager = value;
        }
    }
}