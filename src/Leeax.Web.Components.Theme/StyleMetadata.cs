namespace Leeax.Web.Components.Theme
{
    internal class StyleMetadata
    {
        public StyleMetadata()
        {
        }

        public StyleMetadata(StyleBase value, string stylesheet)
        {
            Value = value;
            Stylesheet = stylesheet;
            CountRendered = 1;
        }

        public StyleBase? Value { get; set; }

        public string? Stylesheet { get; set; }

        public int CountRendered { get; set; }
    }
}
