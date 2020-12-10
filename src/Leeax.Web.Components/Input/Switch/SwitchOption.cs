namespace Leeax.Web.Components.Input
{
    public class SwitchOption : IOption
    {
        public SwitchOption(string? text)
            : this(text, null)
        {
        }

        public SwitchOption(string? text, object? value)
        {
            Text = text;
            Value = value;
        }

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