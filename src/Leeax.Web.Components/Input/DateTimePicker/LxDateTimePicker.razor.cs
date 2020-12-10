using Leeax.Web.Builders;
using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Input
{
    public partial class LxDateTimePicker : IEnableable
    {
        public const string ClassName = "lx-datetimepicker";

        private DateTime _value;
        private string? _format;
        private bool _isFormatSetTroughParameter;
        private DateTimePickerType _type = DateTimePickerType.DateTime;
        private IFormatProvider? _formatProvider = CultureInfo.InvariantCulture;
        private bool _updateShownValue = true;

        public LxDateTimePicker()
        {
            _format = GetFormat();
            Converter = new DateTimeStringConverter(Format);
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .Add(ClassName)
                .Add(ClassNames.Disabled, !IsEnabled));
        }

        private string GetFormat()
        {
            return _type switch
            {
                DateTimePickerType.Date => "yyyy-MM-dd",
                DateTimePickerType.Time => "HH:mm",
                _ => "yyyy-MM-dd HH:mm"
            };
        }

        private void ToggleExtendedState() => IsExtended = !IsExtended;

        public bool IsExtended { get; private set; }

        public DateTime ShownValue { get; private set; }

        /// <summary>
        /// Gets or sets whether the component should be enabled.
        /// </summary>
        [Parameter]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the icon source.
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the appearance. The default value is <see cref="Appearance.Normal"/>.
        /// </summary>
        [Parameter]
        public Appearance Appearance { get; set; } = Appearance.Normal;

        /// <summary>
        /// Gets or sets the size. The default value is <see cref="ComponentSize.Medium"/>.
        /// </summary>
        [Parameter]
        public ComponentSize Size { get; set; } = ComponentSize.Medium;

        /// <summary>
        /// Gets or sets whether the month-picker should be shown.
        /// </summary>
        [Parameter]
        public bool ShowMonthPicker { get; set; }

        /// <summary>
        /// Gets or sets the type(s) to pick.
        /// </summary>
        [Parameter]
        public DateTimePickerType Type
        {
            get => _type;
            set
            {
                _type = value;

                if (!_isFormatSetTroughParameter)
                {
                    _format = GetFormat();

                    if (Converter is DateTimeStringConverter converter)
                    {
                        converter.Format = _format;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Parameter]
        public override DateTime Value
        {
            get => _value;
            set
            {
                _value = value;

                if (_updateShownValue)
                {
                    ShownValue = value;
                    _updateShownValue = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the format for formatting the <see cref="Value"/>.
        /// </summary>
        [Parameter]
        public string? Format
        {
            get => _format;
            set
            {
                _format = value;
                _isFormatSetTroughParameter = true;

                if (Converter is DateTimeStringConverter converter)
                {
                    converter.Format = value;
                }
            }
        }

        /// <summary>
        /// Gets or set the <see cref="IFormatProvider"/> for formatting the <see cref="Value"/>.
        /// </summary>
        [Parameter]
        public IFormatProvider? FormatProvider
        {
            get => _formatProvider;
            set
            {
                _formatProvider = value;

                if (Converter is DateTimeStringConverter converter)
                {
                    converter.FormatProvider = value;
                }
            }
        }
    }
}