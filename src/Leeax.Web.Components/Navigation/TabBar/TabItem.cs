namespace Leeax.Web.Components.Navigation
{
    public class TabItem : IIconOption
    {
        public TabItem(string? text)
            : this(text, null, null)
        {
        }

        public TabItem(string? text, string? icon)
            : this(text, null, icon)
        {
        }

        public TabItem(string? text, object? value, string? icon)
        {
            Text = text;
            Icon = icon;
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