using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components
{
    public interface IIconManager
    {
        /// <summary>
        /// Gets the url for the given resource.
        /// </summary>
        /// <remarks>
        /// Note that the resource url (e.g. <c>http://icons.com/myicon.svg</c>) does not equal the resource uri (e.g. <c>rsrc:myicon</c>).
        /// </remarks>
        /// <param name="resourceUri">An uri pointing to a resource.</param>
        string? GetResourceUrl(Uri? resourceUri);

        /// <summary>
        /// Gets (or fetches) the markup for the given resource, which can be set as content of a &lt;svg&gt; element.
        /// </summary>
        /// <param name="resourceUri">An uri pointing to a resource.</param>
        ValueTask<string?> GetMarkupAsync(Uri? resourceUri);

        /// <summary>
        /// Gets (or fetches) the markup for the (resource) url, which can be set as content of a &lt;svg&gt; element.
        /// </summary>
        /// <param name="url">An url of a &lt;svg&gt; image.</param>
        ValueTask<string?> GetMarkupFromUrlAsync(string? url);
    }
}