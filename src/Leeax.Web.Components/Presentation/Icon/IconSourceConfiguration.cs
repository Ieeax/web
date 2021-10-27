namespace Leeax.Web.Components.Configuration
{
    public class IconSourceConfiguration
    {
        /// <summary>
        /// Gets or sets from which source icons are loaded.
        /// The default value is <see cref="IconSource.Directory"/>.
        /// </summary>
        public IconSource Source { get; set; } = IconSource.Directory;

        /// <summary>
        /// Gets or sets the url of the (SVG) symbol file which contains all icons.
        /// </summary>
        /// <remarks>
        /// Only required when using <see cref="IconSource.SymbolFile"/>.
        /// </remarks>
        public string? SymbolFile { get; set; }

        /// <summary>
        /// Gets or sets the url of the directory from which icons are loaded.
        /// </summary>
        /// <remarks>
        /// Only required when using <see cref="IconSource.Directory"/>.
        /// </remarks>
        public string? Directory { get; set; } = "./";
    }
}