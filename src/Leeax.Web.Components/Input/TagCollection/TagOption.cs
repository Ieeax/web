﻿using System.ComponentModel;

namespace Leeax.Web.Components.Input
{
    public class TagOption : IIconOption, INotifyPropertyChanged
    {
        private string? _icon;
        private string? _text;
        private object? _value;
        private bool _isActive;

        public event PropertyChangedEventHandler? PropertyChanged;

        public TagOption(string? text)
            : this(text, text, null)
        {
        }

        public TagOption(string? text, string? icon)
            : this(text, text, icon)
        {
        }

        public TagOption(string? text, object? value, string? icon)
        {
            _icon = icon;
            _text = text;
            _value = value;
        }

        /// <summary>
        /// Gets or sets the icon source.
        /// </summary>
        public string? Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                PropertyChanged.Raise(this);
            }
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
        /// Gets or sets whether the tag is active.
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                PropertyChanged.Raise(this);
            }
        }
    }
}