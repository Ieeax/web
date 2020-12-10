using Leeax.Web.Components.Configuration;
using Leeax.Web.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Leeax.Web.Components
{
    public class IconProvider : IIconProvider
    {
        private readonly IReadOnlyDictionary<string, IconFactory> _mapping;

        public IconProvider(IconProviderOptions options)
        {
            options.ThrowIfNull();

            _mapping = options.Collection;
        }

        public string? Resolve(string key, Color fillColor)
            => Resolve(key, fillColor.ToHexStr());

        public string? Resolve(string key, string fillColor)
        {
            key.ThrowIfNull();
            fillColor.ThrowIfNull();

            return _mapping.TryGetValue(key, out var creationFunc) 
                ? creationFunc.Invoke(fillColor)
                : null;
        }

        public bool TryResolve(string key, Color fillColor, out string? source)
            => TryResolve(key, fillColor.ToHexStr(), out source);

        public bool TryResolve(string key, string fillColor, out string? source)
        {
            key.ThrowIfNull();
            fillColor.ThrowIfNull();

            if (_mapping.TryGetValue(key, out var creationFunc))
            {
                source = creationFunc.Invoke(fillColor);
                return true;
            }

            source = null;
            return false;
        }

        public IEnumerable<string> IconCollection => _mapping.Keys;
    }
}