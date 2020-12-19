using System;
using System.Drawing;
using System.Globalization;

namespace Leeax.Web.Builders
{
    public static class CssHelper
    {
        public static readonly IFormatProvider FormatProvider = CultureInfo.GetCultureInfo("en-US");

        public static string CreateThicknessValue(Thickness value)
        {
            if (value.Left == value.Right
                && value.Top == value.Bottom)
            {
                if (value.Left == value.Top)
                {
                    return value.Left.ToString();
                }

                return $"{value.Top} {value.Left}";
            }

            return $"{value.Top} {value.Right} {value.Bottom} {value.Left}";
        }

        public static string CreateBoxShadow(params Shadow[]? shadow)
        {
            string? str = null;

            if (shadow != null)
            {
                foreach (var curShadow in shadow)
                {
                    str += CreateBoxShadowValue(curShadow) + ",";
                }
            }

            return str == null
                ? "none"
                : str.TrimEnd(',');
        }

        public static string CreateBoxShadowValue(Shadow shadow)
        {
            if (shadow.OffsetX == 0
                && shadow.OffsetY == 0
                && shadow.BlurRadius == 0
                && shadow.SpreadRadius == 0)
            {
                return "none";
            }

            /* offset-x | offset-y | color */
            /* offset-x | offset-y | blur-radius | color */
            /* offset-x | offset-y | blur-radius | spread-radius | color */
            return $"{shadow.OffsetX} {shadow.OffsetY} {shadow.BlurRadius} {shadow.SpreadRadius} {shadow.Color.ToRgbaStr()}";
        }

        public static string CreateBorderValue(Length thickness, Color color)
        {
            if (thickness.Value != 0)
            {
                return $"{thickness} solid {color.ToRgbaStr()}";
            }

            return "none";
        }
    }
}