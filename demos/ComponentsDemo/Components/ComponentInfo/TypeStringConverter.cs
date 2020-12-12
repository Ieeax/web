#nullable enable
using Leeax.Web.Components;
using System;

namespace ComponentsDemo.Components
{
    public class TypeStringConverter : IConverter<Type, string>
    {
        public bool CanConvertBack(string? value)
        {
            return false;
        }

        public string? Convert(Type? value)
        {
            return value == null ? null : ComponentReflector.GetFriendlyTypeName(value);
        }

        public Type? ConvertBack(string? value)
        {
            return null;
        }
    }
}