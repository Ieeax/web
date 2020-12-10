using System.Drawing;

namespace Leeax.Web.Components.Theme
{
    public static class StyleContextExtensions
    {
        /// <summary>
        /// Tries to get the value associated with the specified key.
        /// </summary>
        public static bool TryGetColor(this StyleContext styleContext, string key, out Color value)
            => styleContext.TryGetValue(key, out value);

        /// <summary>
        /// Tries to get the value associated with the specified key. If not found the <paramref name="defaultValue"/> will be returned.
        /// </summary>
        public static Color GetColorOrDefault(this StyleContext styleContext, string key, Color defaultValue = default)
            => styleContext.GetValueOrDefault(key, defaultValue);

        /// <summary>
        /// Tries to get the color associated with the specified key or fallback-key. If not found the <paramref name="defaultValue"/> will be returned.
        /// </summary>
        public static Color GetColorOrDefault(this StyleContext styleContext, string key, string fallbackKey, Color defaultValue = default)
            => styleContext.GetValueOrDefault(key, fallbackKey, defaultValue);
    }
}