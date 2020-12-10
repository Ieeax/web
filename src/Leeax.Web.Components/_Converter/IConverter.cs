using System;

namespace Leeax.Web.Components
{
    public interface IConverter<TFirst, TSecond> : IConverter
    {
        /// <summary>
        /// Converts a value of type <typeparamref name="TSecond"/> back to <typeparamref name="TFirst"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        TFirst? ConvertBack(TSecond? value);

        /// <summary>
        /// Converts a value of type <typeparamref name="TFirst"/> to <typeparamref name="TSecond"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        TSecond? Convert(TFirst? value);

        /// <summary>
        /// Checks whether the value can be converted back to <typeparamref name="TFirst"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        bool CanConvertBack(TSecond? value);

        object? IConverter.ConvertBack(object? value, Type targetType) 
            => ConvertBack(value is TSecond castedValue ? castedValue : default);

        object? IConverter.Convert(object? value, Type targetType) 
            => Convert(value is TFirst castedValue ? castedValue : default);

        bool IConverter.CanConvertBack(object? value, Type targetType) 
            => CanConvertBack(value is TSecond castedValue ? castedValue : default);
    }

    public interface IConverter
    {
        /// <summary>
        /// Converts a value back to the original <paramref name="targetType"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to convert to.</param>
        object? ConvertBack(object? value, Type targetType);

        /// <summary>
        /// Converts a value to the <paramref name="targetType"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to convert to.</param>
        object? Convert(object? value, Type targetType);

        /// <summary>
        /// Checks whether the value can be converted back to <paramref name="targetType"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="targetType">The type to convert to.</param>
        bool CanConvertBack(object? value, Type targetType);
    }
}