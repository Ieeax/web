namespace Leeax.Web.Components.DOM
{
    // XML comments are taken from https://www.w3schools.com/tags/tag_script.asp
    public class ScriptTagOptions
    {
        public ScriptTagOptions()
        {
        }

        public ScriptTagOptions(string? src)
            : this(src, false)
        {
        }

        public ScriptTagOptions(string? src, bool async)
        {
            Src = src;
            Async = async;
        }

        public ScriptTagOptions(string? src, int timeoutMs)
            : this(src, timeoutMs, null)
        {
        }

        public ScriptTagOptions(string? src, int timeoutMs, string? key)
        {
            Src = src;
            TimeoutMs = timeoutMs;
            Key = key;
        }

        /// <summary>
        /// Specifies that the script is executed asynchronously. (only for external scripts)
        /// </summary>
        public bool Async { get; set; }

        /// <summary>
        /// Specifies that the script is executed when the page has finished parsing. (only for external scripts)
        /// </summary>
        public bool Defer { get; set; }

        /// <summary>
        /// Specifies the URL of an external script file. (required)
        /// </summary>
        public string? Src { get; set; }

        /// <summary>
        /// Specifies the character encoding used in an external script file.
        /// </summary>
        public string? Charset { get; set; }

        /// <summary>
        /// Specifies the media type of the script.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Specifies a timeout in which the script must be loaded.
        /// </summary>
        public int TimeoutMs { get; set; }

        /// <summary>
        /// Specifies a key which can be used to identify the link-tag.
        /// </summary>
        public string? Key { get; set; }

        public static ScriptTagOptions Default => new ScriptTagOptions();
    }
}