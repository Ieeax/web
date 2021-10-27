namespace Leeax.Web.Components.Configuration
{
    public enum IconFetchBehavior
    {
        /// <summary>
        /// Fetches the file once and caches the result, no matter if the request was successful or not.
        /// </summary>
        Once,

        /// <summary>
        /// Tries to fetch the file and only caches it when the request was successful.
        /// </summary>
        UntilSuccessful
    }
}