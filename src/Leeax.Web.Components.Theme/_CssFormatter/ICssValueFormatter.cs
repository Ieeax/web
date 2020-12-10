using System;

namespace Leeax.Web.Components.Theme
{
    public interface ICssValueFormatter
    {
        string Format(Type valueType, object value);
    }

    public interface ICssValueFormatter<T> : ICssValueFormatter
    {
        string Format(Type valueType, T value);

        string ICssValueFormatter.Format(Type valueType, object value) => Format(valueType, (T)value);
    }
}