using Leeax.Web.Components.Input;

namespace Leeax.Web.Components
{
    public class DefaultOptionConverter<TValue> : IConverter<TValue, IIconOption>
    {
        public bool CanConvertBack(IIconOption? value)
            => value != default;

        public IIconOption Convert(TValue? value)
            => new SelectOption(value?.ToString(), value, null); // TODO: Replace "SelectOption" with something generic

        public TValue? ConvertBack(IIconOption? value)
            => value == default ? default : (TValue?)value.Value;

        public static DefaultOptionConverter<TValue> Instance { get; } = new DefaultOptionConverter<TValue>();
    }
}