using System.ComponentModel;

namespace Leeax.Web.Components.Input
{
    public class SortTagOption : IOption, INotifyPropertyChanged
    {
        private string? _text;
        private object? _value;
        private SortDirection _direction;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SortTagOption(string? text)
            : this(text, text, SortDirection.None)
        {
        }

        public SortTagOption(string? text, object? value)
            : this(text, value, SortDirection.None)
        {
        }

        public SortTagOption(string? text, SortDirection direction)
            : this(text, text, direction)
        {
        }

        public SortTagOption(string? text, object? value, SortDirection direction)
        {
            Text = text;
            Value = value;
            Direction = direction;
        }

        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        public string? Text
        {
            get => _text;
            set
            {
                _text = value;
                PropertyChanged.Raise(this);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public object? Value
        {
            get => _value;
            set
            {
                _value = value;
                PropertyChanged.Raise(this);
            }
        }

        /// <summary>
        /// Gets or sets the sort-direction.
        /// </summary>
        public SortDirection Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                PropertyChanged.Raise(this);
            }
        }
    }
}