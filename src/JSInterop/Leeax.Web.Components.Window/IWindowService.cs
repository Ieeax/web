using System.Threading.Tasks;

namespace Leeax.Web.Components.Window
{
    public interface IWindowService
    {
        void Open(string? url, bool openInNewTab);

        void Open(string? url, string? windowName);

        ValueTask OpenAsync(string? url, bool openInNewTab);

        ValueTask OpenAsync(string? url, string? windowName);

        void ShowAlert(string? message);

        bool ShowConfirm(string? message);

        string? ShowPrompt(string? message);

        ValueTask ShowAlertAsync(string? message);

        ValueTask<bool> ShowConfirmAsync(string? message);

        ValueTask<string?> ShowPromptAsync(string? message);

        void ScrollTo(int top, bool smooth = false);

        void ScrollTo(int? top, int? left = null, bool smooth = false);

        ValueTask ScrollToAsync(int top, bool smooth = false);

        ValueTask ScrollToAsync(int? top, int? left = null, bool smooth = false);

        T? GetProperty<T>(string propertyName);

        ValueTask<T?> GetPropertyAsync<T>(string propertyName);

        IEventManager EventManager { get; }
    }
}