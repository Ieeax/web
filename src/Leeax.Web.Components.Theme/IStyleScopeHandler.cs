using System;

namespace Leeax.Web.Components.Theme
{
    public interface IStyleScopeHandler
    {
        void AddFormatter(Type type, ICssValueFormatter formatter);

        void AddFormatter<TValue>(ICssValueFormatter<TValue> formatter);

        void Attach(StyleBase value);

        void Detach(StyleBase value);
    }
}