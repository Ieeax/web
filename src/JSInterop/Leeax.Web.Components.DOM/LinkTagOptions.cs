namespace Leeax.Web.Components.DOM
{
    // XML comments are taken from https://www.w3schools.com/tags/tag_link.asp
    public class LinkTagOptions
    {
        public LinkTagOptions()
        {
        }

        public LinkTagOptions(string? href, string? rel)
            : this(href, rel, null)
        {
        }

        public LinkTagOptions(string? href, string? rel, string? key)
        {
            Href = href;
            Rel = rel;
            Key = key;
        }

        /// <summary>
        /// Specifies how the element handles cross-origin requests.
        /// </summary>
        public string? Crossorigin { get; set; }

        /// <summary>
        /// Specifies the location of the linked document. (required)
        /// </summary>
        public string? Href { get; set; }

        /// <summary>
        /// Specifies the language of the text in the linked document.
        /// </summary>
        public string? HrefLang { get; set; }

        /// <summary>
        /// Specifies on what device the linked document will be displayed.
        /// </summary>
        public string? Media { get; set; }

        /// <summary>
        /// Specifies which referrer to use when fetching the resource.
        /// </summary>
        public string? ReferrerPolicy { get; set; }

        /// <summary>
        /// Specifies the relationship between the current document and the linked document. (required)
        /// </summary>
        public string? Rel { get; set; }

        /// <summary>
        /// Specifies the size of the linked resource. (only for rel="icon")
        /// </summary>
        public string? Sizes { get; set; }

        /// <summary>
        /// Defines a preferred or an alternate stylesheet.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Specifies the media type of the linked document.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Specifies a key which can be used to identify the link-tag.
        /// </summary>
        public string? Key { get; set; }

        public static LinkTagOptions Default => new LinkTagOptions();
    }
}