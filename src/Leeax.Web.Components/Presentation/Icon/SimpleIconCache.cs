using Leeax.Web.Internal;
using System.Collections.Generic;

namespace Leeax.Web.Components
{
    public class SimpleIconCache : IIconCache
    {
        private readonly Dictionary<string, string?> _dictionary;

        public SimpleIconCache()
        {
            _dictionary = new Dictionary<string, string?>();
        }

        /// <inheritdoc/>
        public void AddOrUpdate(string key, string? svgMarkup)
        {
            key.ThrowIfNull();

            _dictionary[key] = svgMarkup;
        }

        /// <inheritdoc/>
        public bool ContainsKey(string key)
        {
            key.ThrowIfNull();

            return _dictionary.ContainsKey(key);
        }

        /// <inheritdoc/>
        public bool TryGetValue(string key, out string? svgMarkup)
        {
            key.ThrowIfNull();

            return _dictionary.TryGetValue(key, out svgMarkup);
        }
    }
}