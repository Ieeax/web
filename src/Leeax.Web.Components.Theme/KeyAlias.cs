using Leeax.Web.Internal;

namespace Leeax.Web.Components.Theme
{
    public class KeyAlias
    {
        public KeyAlias(string value)
        {
            value.ThrowIfNull();
            Value = value;
        }

        public string Value { get; set; }
    }
}