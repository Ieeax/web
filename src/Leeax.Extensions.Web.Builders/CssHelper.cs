using System;
using System.Drawing;
using System.Globalization;
using System.Text;

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

        public static string CreateBoxShadowValue(Dimension offsetX, Dimension offsetY, Dimension blurRadius, Dimension spreadRadius, Color color)
        {
            var builder = new StringBuilder();
            
            builder.Append(offsetX);
            builder.Append(' ');
            builder.Append(offsetX);
            builder.Append(' ');

            if (!blurRadius.IsEmpty)
            {
                builder.Append(blurRadius);
                builder.Append(' ');
            }
            
            if (!spreadRadius.IsEmpty)
            {
                builder.Append(spreadRadius);
                builder.Append(' ');
            }

            builder.Append(color.ToRgbaStr());
            
            /* offset-x | offset-y | color */
            /* offset-x | offset-y | blur-radius | color */
            /* offset-x | offset-y | blur-radius | spread-radius | color */
            return builder.ToString();
        }

        public static string CreateBorderValue(Dimension thickness, Color color)
        {
            if (thickness.Value != 0)
            {
                return $"{thickness} solid {color.ToRgbaStr()}";
            }

            return "none";
        }
    }
}