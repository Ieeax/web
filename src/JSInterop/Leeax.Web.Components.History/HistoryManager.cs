using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Leeax.Web.Components.History
{
    public class HistoryManager : IHistoryManager
    {
        private readonly IJSInProcessRuntime? _jsInProcessRuntime;
        private readonly IJSRuntime _jsRuntime;

        public HistoryManager(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _jsInProcessRuntime = jsRuntime as IJSInProcessRuntime;
        }

        public void NavigateBack()
        {
            _jsInProcessRuntime!.InvokeVoid("window.history.back");
        }

        public void NavigateForward()
        {
            _jsInProcessRuntime!.InvokeVoid("window.history.forward");
        }

        public void Navigate(int value)
        {
            _jsInProcessRuntime!.InvokeVoid("window.history.go", value);
        }

        public ValueTask NavigateBackAsync()
        {
            return _jsRuntime.InvokeVoidAsync("window.history.back");
        }

        public ValueTask NavigateForwardAsync()
        {
            return _jsRuntime.InvokeVoidAsync("window.history.forward");
        }

        public ValueTask NavigateAsync(int value)
        {
            return _jsRuntime.InvokeVoidAsync("window.history.go", value);
        }

        public void PushState(object? state)
            => PushState(state, string.Empty, null); // Note: "title" default is an empty string, see https://developer.mozilla.org/en-US/docs/Web/API/History/pushState
        
        public void PushState(object? state, string? title)
            => PushState(state, title, null);

        public void PushState(object? state, string? title, string? url)
        {
            _jsInProcessRuntime!.InvokeVoid(
                "window.history.pushState",
                state,
                title,
                url);
        }

        public ValueTask PushStateAsync(object? state)
            => PushStateAsync(state, string.Empty, null); // Note: "title" default is an empty string, see https://developer.mozilla.org/en-US/docs/Web/API/History/pushState

        public ValueTask PushStateAsync(object? state, string? title)
            => PushStateAsync(state, title, null);

        public ValueTask PushStateAsync(object? state, string? title, string? url)
        {
            return _jsRuntime.InvokeVoidAsync(
                "window.history.pushState",
                state!,
                title!,
                url!);
        }

        public void ReplaceState(object? state)
            => ReplaceState(state, string.Empty, null); // Note: "title" default is an empty string, see https://developer.mozilla.org/en-US/docs/Web/API/History/replaceState

        public void ReplaceState(object? state, string? title)
            => ReplaceState(state, title, null);

        public void ReplaceState(object? state, string? title, string? url)
        {
            _jsInProcessRuntime!.InvokeVoid(
                "window.history.pushState",
                state!,
                title!,
                url!);
        }

        public ValueTask ReplaceStateAsync(object? state)
            => ReplaceStateAsync(state, string.Empty, null); // Note: "title" default is an empty string, see https://developer.mozilla.org/en-US/docs/Web/API/History/replaceState

        public ValueTask ReplaceStateAsync(object? state, string? title)
            => ReplaceStateAsync(state, title, null);

        public ValueTask ReplaceStateAsync(object? state, string? title, string? url)
        {
            return _jsRuntime.InvokeVoidAsync(
                "window.history.pushState",
                state!,
                title!,
                url!);
        }
    }
}