using System.Threading.Tasks;

namespace Leeax.Web.Components.Clipboard
{
    public interface IClipboard
    {
        /// <summary>
        /// Writes (copies) the specified value to the clipboard.
        /// </summary>
        /// <remarks>
        /// Note that it's recommended to have a secure context (https) and might require permission from the user (see https://developer.mozilla.org/en-US/docs/Mozilla/Add-ons/WebExtensions/Interact_with_the_clipboard#writing_to_the_clipboard).
        /// </remarks>
        ValueTask<bool> WriteAsync(string? value);

        /// <summary>
        /// Reads the last copied value from the clipboard.
        /// </summary>
        /// <remarks>
        /// Note that reading from the clipboard only works in webkit browsers.
        /// Requires a secure context (https) and permission from the user (see https://developer.mozilla.org/en-US/docs/Mozilla/Add-ons/WebExtensions/Interact_with_the_clipboard#reading_from_the_clipboard).
        /// </remarks>
        ValueTask<string?> ReadAsync();
    }
}