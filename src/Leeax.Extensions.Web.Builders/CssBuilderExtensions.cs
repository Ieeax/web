using System.Drawing;

namespace Leeax.Web.Builders
{
    public static class CssBuilderExtensions
    {
        public static ICssBuilder AddProperty(this ICssBuilder builder, string property, int value, bool when = true)
        {
            return builder.AddProperty(property, value.ToString(), when);
        }

        public static ICssBuilder AddProperty(this ICssBuilder builder, string property, double value, bool when = true)
        {
            return builder.AddProperty(property, value.ToString(CssHelper.FormatProvider), when);
        }

        public static ICssBuilder AddColor(this ICssBuilder builder, Color color, bool when = true)
        {
            return builder.AddProperty("color", color.ToRgbaStr(), when);
        }

        public static ICssBuilder AddBackground(this ICssBuilder builder, Color backgroundColor, bool when = true)
        {
            return builder.AddProperty("background", backgroundColor.ToRgbaStr(), when);
        }

        public static ICssBuilder AddThickness(this ICssBuilder builder, string property, Thickness thickness, bool when = true)
        {
            return when
                ? builder.AddProperty(property, CssHelper.CreateThicknessValue(thickness))
                : builder;
        }

        public static ICssBuilder AddPadding(this ICssBuilder builder, Thickness thickness, bool when = true)
        {
            return AddThickness(builder, "padding", thickness, when);
        }

        public static ICssBuilder AddPaddingLeft(this ICssBuilder builder, Length value, bool when = true)
        {
            return AddDimension(builder, "padding-left", value, when);
        }

        public static ICssBuilder AddPaddingRight(this ICssBuilder builder, Length value, bool when = true)
        {
            return AddDimension(builder, "padding-right", value, when);
        }

        public static ICssBuilder AddPaddingTop(this ICssBuilder builder, Length value, bool when = true)
        {
            return AddDimension(builder, "padding-top", value, when);
        }

        public static ICssBuilder AddPaddingBottom(this ICssBuilder builder, Length value, bool when = true)
        {
            return AddDimension(builder, "padding-bottom", value, when);
        }

        public static ICssBuilder AddMargin(this ICssBuilder builder, Thickness thickness, bool when = true)
        {
            return AddThickness(builder, "margin", thickness, when);
        }

        public static ICssBuilder AddMarginLeft(this ICssBuilder builder, Length value, bool when = true)
        {
            return AddDimension(builder, "margin-left", value, when);
        }

        public static ICssBuilder AddMarginRight(this ICssBuilder builder, Length value, bool when = true)
        {
            return AddDimension(builder, "margin-right", value, when);
        }

        public static ICssBuilder AddMarginTop(this ICssBuilder builder, Length value, bool when = true)
        {
            return AddDimension(builder, "margin-top", value, when);
        }

        public static ICssBuilder AddMarginBottom(this ICssBuilder builder, Length value, bool when = true)
        {
            return AddDimension(builder, "margin-bottom", value, when);
        }

        public static ICssBuilder AddBorderRadius(this ICssBuilder builder, Thickness thickness, bool when = true)
        {
            return AddThickness(builder, "border-radius", thickness, when);
        }

        public static ICssBuilder AddBorderWidth(this ICssBuilder builder, Thickness thickness, bool when = true)
        {
            return AddThickness(builder, "border-width", thickness, when);
        }

        public static ICssBuilder AddBorder(this ICssBuilder builder, Length thickness, Color color, bool when = true)
        {
            return when
                ? builder.AddProperty("border", CssHelper.CreateBorderValue(thickness, color))
                : builder;
        }

        public static ICssBuilder AddBorderColor(this ICssBuilder builder, Color color, bool when = true)
        {
            return builder.AddProperty("border-color", color.ToRgbaStr(), when);
        }

        public static ICssBuilder AddBorderStyle(this ICssBuilder builder, string? style, bool when = true)
        {
            return builder.AddProperty("border-style", style, when);
        }

        public static ICssBuilder AddShadow(this ICssBuilder builder, Shadow shadow, bool when = true)
        {
            return when
                ? builder.AddProperty("box-shadow", CssHelper.CreateBoxShadowValue(shadow))
                : builder;
        }

        public static ICssBuilder AddShadow(this ICssBuilder builder, Shadow[] shadow, bool when = true)
        {
            return when
                ? builder.AddProperty("box-shadow", CssHelper.CreateBoxShadow(shadow))
                : builder;
        }

        public static ICssBuilder AddFlexBasis(this ICssBuilder builder, Length value, bool when = true)
        {
            return builder.AddProperty("flex-basis", value.ToString(), when);
        }

        public static ICssBuilder AddFlexGrow(this ICssBuilder builder, int value, bool when = true)
        {
            return (when && value >= 0)
                ? builder.AddProperty("flex-grow", value.ToString())
                : builder;
        }

        public static ICssBuilder AddFlexShrink(this ICssBuilder builder, int value, bool when = true)
        {
            return (when && value >= 0)
                ? builder.AddProperty("flex-shrink", value.ToString())
                : builder;
        }

        public static ICssBuilder AddFlex(this ICssBuilder builder, Length basis, int grow, int shrink, bool when = true)
        {
            if (!when)
            {
                return builder;
            }

            return builder
                .AddProperty("flex-basis", basis.ToString())
                .AddProperty("flex-grow", grow.ToString(), grow >= 0)
                .AddProperty("flex-shrink", shrink.ToString(), shrink >= 0);
        }

        public static ICssBuilder AddDimension(this ICssBuilder builder, string property, Length value, bool when = true)
        {
            return value.IsEmpty
                ? builder
                : builder.AddProperty(property, value.ToString(), when);
        }

        public static ICssBuilder AddHeight(this ICssBuilder builder, Length value, bool when = true)
        {
            return AddDimension(builder, "height", value, when);
        }

        public static ICssBuilder AddWidth(this ICssBuilder builder, Length value, bool when = true)
        {
            return AddDimension(builder, "width", value, when);
        }
    }
}