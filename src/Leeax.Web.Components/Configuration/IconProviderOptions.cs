using Leeax.Web.Internal;
using System.Collections.Generic;

namespace Leeax.Web.Components.Configuration
{
    public class IconProviderOptions
    {
        private readonly Dictionary<string, IconFactory> _mapping;

        public IconProviderOptions()
        {
            _mapping = new Dictionary<string, IconFactory>();
        }

        public void Add(string key, IconFactory factory)
        {
            key.ThrowIfNull();
            factory.ThrowIfNull();

            _mapping[key] = factory;
        }

        public IReadOnlyDictionary<string, IconFactory> Collection => _mapping;
    }

    public delegate string IconFactory(string colorHex);
}