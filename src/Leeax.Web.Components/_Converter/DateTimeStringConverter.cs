using System;
using System.Globalization;

namespace Leeax.Web.Components
{
    public class DateTimeStringConverter : IConverter<DateTime, string?>
    {
        public DateTimeStringConverter()
        {
        }

        public DateTimeStringConverter(string? format)
            : this(format, null, DateTimeStyles.None)
        {
        }

        public DateTimeStringConverter(string? format, IFormatProvider? formatProvider)
            : this(format, formatProvider, DateTimeStyles.None)
        {
        }

        public DateTimeStringConverter(string? format, IFormatProvider? formatProvider, DateTimeStyles dateTimeStyles)
        {
            Format = format;
            FormatProvider = formatProvider;
            DateTimeStyles = dateTimeStyles;
        }

        public string Convert(DateTime value)
        {
            return value.ToString(Format);
        }

        public DateTime ConvertBack(string? value)
        {
            return DateTime.TryParseExact(value, Format, FormatProvider, DateTimeStyles, out var result)
                ? result
                : DateTime.MinValue;
        }

        public bool CanConvertBack(string? value)
        {
            return DateTime.TryParseExact(value, Format, FormatProvider, DateTimeStyles, out _);
        }

        /// <summary>
        /// Gets or sets the format which is used when converting to <see cref="string"/>.
        /// </summary>
        public string? Format { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.Globalization.DateTimeStyles"/> which are used when converting to <see cref="DateTime"/>.
        /// </summary>
        public DateTimeStyles DateTimeStyles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.IFormatProvider"/> which is used when converting to <see cref="DateTime"/>.
        /// </summary>
        public IFormatProvider? FormatProvider { get; set; }
    }
}