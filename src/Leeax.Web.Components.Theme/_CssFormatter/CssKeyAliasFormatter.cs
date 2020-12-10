using System;

namespace Leeax.Web.Components.Theme
{
    public class CssKeyAliasFormatter : ICssValueFormatter<KeyAlias>
    {
        public string Format(Type valueType, KeyAlias value) 
            => "var(--" + value.Value + ")";
    }
}