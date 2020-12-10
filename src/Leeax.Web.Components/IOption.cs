namespace Leeax.Web.Components
{
    public interface IOption
    {
        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        string? Text { get; }

        /// <summary>
        /// Gets or sets the underlying value.
        /// </summary>
        object? Value { get; }
    }
}