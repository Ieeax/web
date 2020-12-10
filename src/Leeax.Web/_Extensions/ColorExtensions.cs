using System;
using System.Drawing;
using System.Globalization;

namespace Leeax.Web
{
    public static class ColorExtensions
    {
        public static string ToHexStr(this Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        public static string ToRgbStr(this Color color)
        {
            return "rgb(" + color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString() + ")";
        }

        public static string ToRgbaStr(this Color color)
        {
            return "rgba(" + color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString() + "," + Math.Round(((double)color.A / 255), 2, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture) + ")";
        }
    }
}