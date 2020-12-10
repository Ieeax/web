using Leeax.Web.Internal;
using System;
using System.Drawing;
using System.Globalization;

namespace Leeax.Web
{
    public static class ColorHelper
    {
        internal static readonly IFormatProvider FORMAT_PROVIDER = CultureInfo.GetCultureInfo("en-US");

        public static Color FromHexStr(string hexStr)
        {
            hexStr.ThrowIfNull();

            //Remove # if present
            if (hexStr.IndexOf('#') != -1)
            {
                hexStr = hexStr.TrimStart('#');
            }

            int red;
            int green;
            int blue;

            if (hexStr.Length == 6)
            {
                //#RRGGBB
                red = int.Parse(hexStr.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexStr.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexStr.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            else if (hexStr.Length == 3)
            {
                //#RGB
                red = int.Parse(hexStr[0].ToString() + hexStr[0].ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexStr[1].ToString() + hexStr[1].ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexStr[2].ToString() + hexStr[2].ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            else
            {
                return Color.Transparent;
            }

            return Color.FromArgb(red, green, blue);
        }

        public static Color FromRgbStr(string rbgStr)
        {
            rbgStr.ThrowIfNull();

            if (rbgStr.IndexOf("rgb(") != -1)
            {
                var colorStr = rbgStr[4..^1];
                var values = colorStr.Split(',');

                if (values.Length != 3)
                {
                    return Color.Transparent;
                }

                var parsedValues = new byte[3];

                for (int i = 0; i < values.Length; i++)
                {
                    parsedValues[i] = byte.Parse(values[i].ToString());
                }

                return Color.FromArgb(
                    255,
                    parsedValues[0],
                    parsedValues[1],
                    parsedValues[2]);
            }
            
            return Color.Transparent;
        }

        public static Color FromRgbaStr(string rbgStr)
        {
            rbgStr.ThrowIfNull();

            if (rbgStr.IndexOf("rgba(") != -1)
            {
                var colorStr = rbgStr[5..^1];
                var values = colorStr.Split(',');

                if (values.Length != 4)
                {
                    return Color.Transparent;
                }

                var parsedValues = new byte[4];

                for (int i = 0; i < values.Length; i++)
                {
                    // Handle alpha-channel
                    if (i == 3)
                    {
                        var alpha = double.Parse(values[i], FORMAT_PROVIDER);

                        // ARGB uses 255, RGBA uses 1, so convert here
                        parsedValues[i] = (byte)Math.Round((255 * alpha), 0, MidpointRounding.AwayFromZero);
                        break;
                    }

                    parsedValues[i] = byte.Parse(values[i].ToString());
                }

                return Color.FromArgb(
                    parsedValues[3],
                    parsedValues[0],
                    parsedValues[1],
                    parsedValues[2]);
            }

            return Color.Transparent;
        }

        public static bool IsLightColor(Color color)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32(
                (color.R * 0.299) +
                (color.G * 0.587) +
                (color.B * 0.114));

            return (255 - bgDelta < nThreshold);
        }

        public static bool IsDarkColor(Color color)
        {
            //return !IsLightColor(color);
            var luminance = 0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B;

            return luminance < 128;
        }
    }
}