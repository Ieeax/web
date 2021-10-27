using Leeax.Web.Components.Configuration;
using Leeax.Web.Internal;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Leeax.Web.Components
{
    public class IconManager : IIconManager
    {
        private readonly IconOptions _options;
        private readonly HttpClient _httpClient;
        private readonly IIconCache _cache;

        public IconManager(HttpClient httpClient, IIconCache cache, IconOptions options)
        {
            options.ThrowIfNull();

            _httpClient = httpClient;
            _cache = cache;
            _options = options;
        }

        /// <summary>
        /// Gets the (source) configuration for the given resource.
        /// </summary>
        /// <param name="resourceUri">An uri pointing to a resource.</param>
        /// <param name="configuration">The (source) configuration for the resource.</param>
        private bool TryGetResourceSource(Uri? resourceUri, [MaybeNullWhen(false)] out IconSourceConfiguration configuration)
        {
            configuration = null;

            return resourceUri != null
                && resourceUri.IsAbsoluteUri
                && resourceUri.Scheme == "rsrc"
                && !string.IsNullOrEmpty(resourceUri.AbsolutePath.Trim('/'))
                && _options.TryGetSource(resourceUri.Authority, out configuration);
        }

        /// <summary>
        /// Gets the url for the given resource path and configuration.
        /// </summary>
        /// <remarks>
        /// Note that the resource url (e.g. <c>http://icons.com/myicon.svg</c>) does not equal the resource uri (e.g. <c>rsrc:myicon</c>).
        /// </remarks>
        /// <param name="resourcePath">The path of the resource.</param>
        /// <param name="configuration">The (source) configuration for the resource.</param>
        private string GetResourceUrl(string resourcePath, IconSourceConfiguration configuration)
        {
            // Remove preceding slashes
            resourcePath = resourcePath.TrimStart('/');

            switch (configuration.Source)
            {
                case IconSource.SymbolFile:
                    return configuration.SymbolFile + "#" + resourcePath;

                case IconSource.Directory:

                    // Prepare base path
                    var basePath = configuration.Directory;
                    if (!string.IsNullOrEmpty(basePath)
                        && !basePath.EndsWith("/"))
                    {
                        basePath += "/";
                    }

                    return basePath + (Path.HasExtension(resourcePath) ? resourcePath : resourcePath + ".svg");

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <inheritdoc/>
        public string? GetResourceUrl(Uri? resourceUri)
        {
            return TryGetResourceSource(resourceUri, out var configuration) 
                ? GetResourceUrl(resourceUri!.AbsolutePath, configuration)
                : null;
        }

        /// <inheritdoc/>
        public ValueTask<string?> GetMarkupAsync(Uri? resourceUri)
        {
            if (!TryGetResourceSource(resourceUri, out var configuration))
            {
                return ValueTask.FromResult<string?>(null);
            }

            if (configuration.Source == IconSource.SymbolFile)
            {
                return ValueTask.FromResult<string?>(
                    $"<use href=\"{GetResourceUrl(resourceUri!.AbsolutePath, configuration)}\"></use>");
            }

            return GetMarkupFromUrlAsync(
                GetResourceUrl(resourceUri!.AbsolutePath, configuration));
        }

        /// <inheritdoc/>
        public async ValueTask<string?> GetMarkupFromUrlAsync(string? url)
        {
            if (url == null)
            {
                return null;
            }

            if (_cache.TryGetValue(url, out var markup))
            {
                return markup;
            }

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                markup = await response.Content.ReadAsStringAsync();

                // Add fetched svg to cache
                _cache.AddOrUpdate(url, markup);

                return markup;
            }
            else if (_options.IconFetchBehavior == IconFetchBehavior.Once)
            {
                // Add entry to cache, so that the file won't get fetched again
                _cache.AddOrUpdate(url, null);
            }

            return null;
        }
    }
}