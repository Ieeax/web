namespace Leeax.Web.Components
{
    internal static class ConverterExtensions
    {
        public static TValue? ConvertBackTypeSafe<TValue>(this IConverter converter, object? value)
        {
            var result = converter.ConvertBack(value, typeof(TValue));
            return result is TValue castedResult ? castedResult : default;
        }

        public static TValue? ConvertTypeSafe<TValue>(this IConverter converter, object? value)
        {
            var result = converter.Convert(value, typeof(TValue));
            return result is TValue castedResult ? castedResult : default;
        }

        public static bool CanConvertBackTypeSafe<TValue>(this IConverter converter, object? value)
        {
            return converter.CanConvertBack(value, typeof(TValue));
        }
    }
}