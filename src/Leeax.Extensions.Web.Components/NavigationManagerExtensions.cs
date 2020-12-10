using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;

namespace Leeax.Extensions.Web.Components
{
    public static class NavigationManagerExtensions
    {
        public static void NavigateTo(this NavigationManager navManager, string uri, Action<QueryBuilder> queryBuilderFactory, bool forceLoad = false)
        {
            navManager.ThrowIfNull();
            uri.ThrowIfNull();
            queryBuilderFactory.ThrowIfNull();

            var uriObj = new Uri(uri);
            var parameters = QueryHelpers.ParseNullableQuery(uriObj.Query);

            // Create builder with parameters from passed uri
            var queryBuilder = parameters == null
                ? new QueryBuilder()
                : new QueryBuilder(parameters);
            
            // Invoke builder factory
            queryBuilderFactory.Invoke(queryBuilder);

            var formattedUri = uriObj.GetLeftPart(UriPartial.Path) + queryBuilder.ToString();

            // Navigate to uri
            navManager.NavigateTo(formattedUri, forceLoad);
        }

        public static void NavigateTo(this NavigationManager navManager, Action<QueryBuilder> queryBuilderFactory, bool forceLoad = false)
            => NavigateTo(navManager, navManager.Uri, queryBuilderFactory, forceLoad);

        public static bool TryGetQueryValue(this NavigationManager navManager, string key, out StringValues value)
        {
            navManager.ThrowIfNull();
            key.ThrowIfNull();

            var uri = navManager.ToAbsoluteUri(navManager.Uri);

            return QueryHelpers
                .ParseQuery(uri.Query)
                .TryGetValue(key, out value);
        }

        public static bool TryGetQueryValue<T>(this NavigationManager navManager, string key, QueryParseOptions? options, out T? value)
        {
            navManager.ThrowIfNull();
            key.ThrowIfNull();

            options ??= QueryParseOptions.Default;

            if (navManager.TryGetQueryValue(key, out var valueFromQueryString))
            {
                if (typeof(T) == typeof(string))
                {
                    value = (T)(object)valueFromQueryString.ToString();
                }
                else if (typeof(T) == typeof(bool) 
                    && bool.TryParse(valueFromQueryString, out var valueAsBool))
                {
                    value = (T)(object)valueAsBool;
                }
                else if (typeof(T) == typeof(byte) 
                    && byte.TryParse(valueFromQueryString, options.NumberStyles, options.FormatProvider, out var valueAsByte))
                {
                    value = (T)(object)valueAsByte;
                }
                else if (typeof(T) == typeof(sbyte) 
                    && sbyte.TryParse(valueFromQueryString, options.NumberStyles, options.FormatProvider, out var valueAsSByte))
                {
                    value = (T)(object)valueAsSByte;
                }
                else if (typeof(T) == typeof(char) 
                    && char.TryParse(valueFromQueryString, out var valueAsChar))
                {
                    value = (T)(object)valueAsChar;
                }
                else if (typeof(T) == typeof(decimal) 
                    && decimal.TryParse(valueFromQueryString, options.NumberStyles, options.FormatProvider, out var valueAsDecimal))
                {
                    value = (T)(object)valueAsDecimal;
                }
                else if (typeof(T) == typeof(double) 
                    && double.TryParse(valueFromQueryString, options.NumberStyles, options.FormatProvider, out var valueAsDouble))
                {
                    value = (T)(object)valueAsDouble;
                }
                else if (typeof(T) == typeof(float) 
                    && float.TryParse(valueFromQueryString, options.NumberStyles, options.FormatProvider, out var valueAsFloat))
                {
                    value = (T)(object)valueAsFloat;
                }
                else if (typeof(T) == typeof(int) 
                    && int.TryParse(valueFromQueryString, options.NumberStyles, options.FormatProvider, out var valueAsInt))
                {
                    value = (T)(object)valueAsInt;
                }
                else if (typeof(T) == typeof(uint) 
                    && uint.TryParse(valueFromQueryString, options.NumberStyles, options.FormatProvider, out var valueAsUInt))
                {
                    value = (T)(object)valueAsUInt;
                }
                else if (typeof(T) == typeof(long) 
                    && long.TryParse(valueFromQueryString, options.NumberStyles, options.FormatProvider, out var valueAsLong))
                {
                    value = (T)(object)valueAsLong;
                }
                else if (typeof(T) == typeof(ulong) 
                    && ulong.TryParse(valueFromQueryString, options.NumberStyles, options.FormatProvider, out var valueAsULong))
                {
                    value = (T)(object)valueAsULong;
                }
                else if (typeof(T) == typeof(short) 
                    && short.TryParse(valueFromQueryString, options.NumberStyles, options.FormatProvider, out var valueAsShort))
                {
                    value = (T)(object)valueAsShort;
                }
                else if (typeof(T) == typeof(ushort) 
                    && ushort.TryParse(valueFromQueryString, options.NumberStyles, options.FormatProvider, out var valueAsUShort))
                {
                    value = (T)(object)valueAsUShort;
                }
                else if (typeof(T) == typeof(Guid) 
                    && Guid.TryParseExact(valueFromQueryString, options.Format, out var valueAsGuid))
                {
                    value = (T)(object)valueAsGuid;
                }
                else if (typeof(T) == typeof(DateTime) 
                    && DateTime.TryParseExact(valueFromQueryString, options.Format, options.FormatProvider, options.DateTimeStyles, out var valueAsDateTime))
                {
                    value = (T)(object)valueAsDateTime;
                }
                else if (typeof(T) == typeof(TimeSpan) 
                    && TimeSpan.TryParseExact(valueFromQueryString, options.Format, options.FormatProvider, out var valueAsTimeSpan))
                {
                    value = (T)(object)valueAsTimeSpan;
                }
                else if (typeof(T) == typeof(StringValues))
                {
                    value = (T)(object)valueFromQueryString;
                }
                else
                {
                    value = default;
                    return false;
                }

                return true;
            }

            value = default;
            return false;
        }

        public static bool TryGetQueryValue<T>(this NavigationManager navManager, string key, out T? value)
        {
            navManager.ThrowIfNull();
            key.ThrowIfNull();

            return TryGetQueryValue<T>(navManager, key, QueryParseOptions.Default, out value);
        }

        public static T? GetQueryValueOrDefault<T>(this NavigationManager navManager, string key, QueryParseOptions? options, T? defaultValue = default)
        {
            navManager.ThrowIfNull();
            key.ThrowIfNull();

            return navManager.TryGetQueryValue<T>(key, options, out var value)
                ? value
                : defaultValue;
        }

        public static T? GetQueryValueOrDefault<T>(this NavigationManager navManager, string key, T? defaultValue = default)
        {
            navManager.ThrowIfNull();
            key.ThrowIfNull();

            return navManager.TryGetQueryValue<T>(key, out var value)
                ? value
                : defaultValue;
        }
    }
}