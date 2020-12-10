using Leeax.Web.Internal;
using System;
using System.Collections.Generic;

namespace Leeax.Web.Components.Theme
{
    public abstract class StyleBase
    {
        private readonly IReadOnlyDictionary<string, object?> _properties;

        public StyleBase()
            : this(Guid.NewGuid().ToString("N"))
        {
        }

        public StyleBase(string identifier)
        {
            identifier.ThrowIfNull();

            Identifier = identifier;

            // Create the palette during constructor
            var builder = new StyleBuilder();
            BuildStyle(builder);

            _properties = builder.Build();
        }

        /// <summary>
        /// Creates the <see cref="ColorPalette"/> itself. Gets automatically called in the constructor.
        /// </summary>
        /// <param name="builder">The builder to which the colors have to be added.</param>
        public abstract void BuildStyle(StyleBuilder builder);

        /// <summary>
        /// Gets the color which is associated with the specified key.
        /// </summary>
        public bool TryGetValue(string key, out object? value)
        {
            // Check if colors are set
            // -> Only possible before/during calling "BuildColorPalette"
            if (_properties == null)
            {
                value = default;
                return false;
            }

            return _properties.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets the object as dictionary. This method returns always the same instance.
        /// </summary>
        public IReadOnlyDictionary<string, object?> ToDictionary() => _properties;

        /// <summary>
        /// Gets the identifier for the palette.
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// Gets an empty instance.
        /// </summary>
        public static StyleBase Empty { get; } = new EmptyStyle();
    }
}
