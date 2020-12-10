using Leeax.Web;
using Leeax.Web.Components;
using System.Drawing;

namespace ComponentsDemo
{
    public class ColorStringConverter : IConverter<Color, string>
    {
        public string Convert(Color value)
            => value.ToHexStr();

        public Color ConvertBack(string value)
            => ColorHelper.FromHexStr(value);

        public bool CanConvertBack(string value)
            => value != null && ConvertBack(value) != Color.Transparent;
    }
}