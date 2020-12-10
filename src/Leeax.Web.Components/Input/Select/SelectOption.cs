namespace Leeax.Web.Components.Input
{
    public class SelectOption : IIconOption
    {
        public SelectOption(string? text)
            : this(text, text, null)
        {
        }

        public SelectOption(string? text, string? icon)
            : this(text, text, icon)
        {
        }

        public SelectOption(string? text, object? value, string? icon)
        {
            Icon = icon;
            Text = text;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the icon source.
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public object? Value { get; set; }
    }
}