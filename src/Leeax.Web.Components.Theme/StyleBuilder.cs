using Leeax.Web.Internal;
using System.Collections.Generic;

namespace Leeax.Web.Components.Theme
{
    public class StyleBuilder
    {
        private readonly Dictionary<string, object?> _properties;

        public StyleBuilder()
            => _properties = new Dictionary<string, object?>();

        /// <summary>
        /// Adds the specified <paramref name="key"/> and <paramref name="value"/> to the builder.
        /// </summary>
        public void Add(string key, object? value)
        {
            _properties.Add(key, value!);
        }

        /// <summary>
        /// Adds the specified <paramref name="aliasKey"/> as an alias for <paramref name="key"/>.
        /// </summary>
        public void AddAlias(string aliasKey, string key)
        {
            _properties.Add(aliasKey, new KeyAlias(key));
        }

        public IReadOnlyDictionary<string, object?> Build()
            => _properties;
    }
}