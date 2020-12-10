using System;
using System.Globalization;

namespace Leeax.Extensions.Web.Components
{
    public class QueryParseOptions
    {
        public QueryParseOptions()
        {
            FormatProvider = CultureInfo.InvariantCulture;
        }

        public IFormatProvider? FormatProvider { get; set; }

        public DateTimeStyles DateTimeStyles { get; set; }

        public NumberStyles NumberStyles { get; set; }

        public string? Format { get; set; }

        public static QueryParseOptions Default { get; } = new QueryParseOptions();
    }
}