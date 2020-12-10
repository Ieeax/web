using System;
using System.Globalization;

namespace Leeax.Web.Components
{
    public class DoubleStringConverter : IConverter<double, string>
    {
        public DoubleStringConverter()
        {
        }

        public DoubleStringConverter(string? format)
            : this(format, null, NumberStyles.Float)
        {
        }

        public DoubleStringConverter(string? format, IFormatProvider? formatProvider)
            : this(format, formatProvider, NumberStyles.Float)
        {
        }

        public DoubleStringConverter(string? format, IFormatProvider? formatProvider, NumberStyles numberStyles)
        {
            Format = format;
            FormatProvider = formatProvider;
            NumberStyles = numberStyles;
        }

        public string Convert(double value)
        {
            return value.ToString(Format, FormatProvider);
        }

        public double ConvertBack(string? value)
        {
            if (value == null)
            {
                return 0;
            }

            return (double.TryParse(value.ToString(), NumberStyles, FormatProvider, out var result))
                ? result
                : 0;
        }

        public bool CanConvertBack(string? value)
        {
            if (value == null)
            {
                return true;
            }

            return double.TryParse(value.ToString(), NumberStyles, FormatProvider, out _);
        }

        /// <summary>
        /// Gets or sets the format which is used when converting to <see cref="string"/>.
        /// </summary>
        public string? Format { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.Globalization.NumberStyles"/> which are used when converting to <see cref="double"/>.
        /// </summary>
        public NumberStyles NumberStyles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.IFormatProvider"/> which is used when converting to <see cref="double"/>.
        /// </summary>
        public IFormatProvider? FormatProvider { get; set; }
    }
}