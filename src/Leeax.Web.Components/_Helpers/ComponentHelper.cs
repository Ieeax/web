using System.Drawing;

namespace Leeax.Web.Components
{
    internal static class ComponentHelper
    {
        public static Color GetForegroundColor(Appearance appearance, Color backgroundColor, Color colorWhenTransparent)
        {
            return appearance == Appearance.Outlined 
                || appearance == Appearance.Normal 
                || backgroundColor == Color.Transparent

                ? colorWhenTransparent
                : ColorHelper.IsDarkColor(backgroundColor)
                    ? Color.White
                    : Color.Black;
        }
    }
}