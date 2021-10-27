namespace Leeax.Web.Components
{
    public interface IIconCache
    {
        /// <summary>
        /// Adds or updates the markup for the given key.
        /// </summary>
        public void AddOrUpdate(string key, string? svgMarkup);

        /// <summary>
        /// Checks whether an entry for the given key exists.
        /// </summary>
        public bool ContainsKey(string key);

        /// <summary>
        /// Tries to get the markup for the given key.
        /// </summary>
        public bool TryGetValue(string key, out string? svgMarkup);
    }
}