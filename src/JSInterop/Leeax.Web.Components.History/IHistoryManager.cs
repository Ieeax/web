using System.Threading.Tasks;

namespace Leeax.Web.Components.History
{
    public interface IHistoryManager
    {
        void NavigateBack();

        void NavigateForward();

        void Navigate(int value);

        ValueTask NavigateBackAsync();

        ValueTask NavigateForwardAsync();

        ValueTask NavigateAsync(int value);

        void PushState(object? state);

        void PushState(object? state, string? title);

        void PushState(object? state, string? title, string? url);

        ValueTask PushStateAsync(object? state);

        ValueTask PushStateAsync(object? state, string? title);

        ValueTask PushStateAsync(object? state, string? title, string? url);

        void ReplaceState(object? state);

        void ReplaceState(object? state, string? title);

        void ReplaceState(object? state, string? title, string? url);

        ValueTask ReplaceStateAsync(object? state);
                  
        ValueTask ReplaceStateAsync(object? state, string? title);
                  
        ValueTask ReplaceStateAsync(object? state, string? title, string? url);
    }
}