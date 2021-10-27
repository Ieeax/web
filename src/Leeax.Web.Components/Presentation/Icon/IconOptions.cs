using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Leeax.Web.Components.Configuration
{
    public class IconOptions
    {
        private const string DefaultSource = "app";
        private readonly Dictionary<string, IconSourceConfiguration> _icons = new();

        /// <summary>
        /// Adds a directory (containing SVG icons) as the default icon source.
        /// </summary>
        /// <param name="directory">The relative or absolute url to the directory.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddDirectory(string directory)
        {
            AddSource(DefaultSource, new IconSourceConfiguration
            {
                Directory = directory,
                Source = IconSource.Directory
            });
        }

        /// <summary>
        /// Adds a directory (containing SVG icons) as icon source.
        /// </summary>
        /// <param name="source">The uri authority component for the icon source. Passing <see langword="null"/> sets the default source.</param>
        /// <param name="directory">The relative or absolute url to the directory.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddDirectory(string source, string directory)
        {
            AddSource(source, new IconSourceConfiguration
            {
                Directory = directory,
                Source = IconSource.Directory
            });
        }

        /// <summary>
        /// Adds a (SVG) symbol file as the default icon source.
        /// </summary>
        /// <param name="fileLocation">The relative or absolute url of the symbol file.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddSymbolFile(string fileLocation)
        {
            AddSource(DefaultSource, new IconSourceConfiguration
            {
                SymbolFile = fileLocation,
                Source = IconSource.SymbolFile
            });
        }

        /// <summary>
        /// Adds a (SVG) symbol file as icon source.
        /// </summary>
        /// <param name="source">The uri authority component for the icon source. Passing <see langword="null"/> sets the default source.</param>
        /// <param name="fileLocation">The relative or absolute url of the symbol file.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddSymbolFile(string? source, string? fileLocation)
        {
            AddSource(source, new IconSourceConfiguration
            {
                SymbolFile = fileLocation ?? string.Empty,
                Source = IconSource.SymbolFile
            });
        }

        /// <summary>
        /// Adds an icon source with the given configuration.
        /// </summary>
        /// <param name="source">The uri authority component for the icon source. Passing <see langword="null"/> sets the default source.</param>
        /// <param name="value">The configuration for the given source.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        private void AddSource(string? source, IconSourceConfiguration configuration)
        {
            source ??= DefaultSource;

            if (_icons.ContainsKey(source))
            {
                throw new ArgumentException("An icon source with the same key already exists.");
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _icons.Add(source.ToLowerInvariant(), configuration);
        }

        /// <summary>
        /// Gets the configuration for the given icon source.
        /// </summary>
        /// <param name="source">The uri authority component of the icon source.</param>
        /// <param name="value">The configuration for the given source.</param>
        public bool TryGetSource(string? source, [MaybeNullWhen(false)] out IconSourceConfiguration value)
        {
            return _icons.TryGetValue((string.IsNullOrEmpty(source) ? DefaultSource : source).ToLowerInvariant(), out value);
        }

        /// <summary>
        /// Gets or sets how SVG images are fetched when the source is set to <see cref="IconSource.Directory"/>.
        /// The default value is <see cref="IconFetchBehavior.Once"/>.
        /// </summary>
        public IconFetchBehavior IconFetchBehavior { get; set; } = IconFetchBehavior.Once;
    }
}