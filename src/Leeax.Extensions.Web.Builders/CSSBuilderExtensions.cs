using System.Drawing;

namespace Leeax.Web.Builders
{
    public static class CSSBuilderExtensions
    {
        public static ICSSBuilder AddProperty(this ICSSBuilder builder, string property, int value, bool when = true)
        {
            return builder.AddProperty(property, value.ToString(), when);
        }

        public static ICSSBuilder AddProperty(this ICSSBuilder builder, string property, double value, bool when = true)
        {
            return builder.AddProperty(property, value.ToString(CSSHelper.FormatProvider), when);
        }

        public static ICSSBuilder AddColor(this ICSSBuilder builder, Color color, bool when = true)
        {
            return builder.AddProperty("color", color.ToRgbaStr(), when);
        }

        public static ICSSBuilder AddBackground(this ICSSBuilder builder, Color backgroundColor, bool when = true)
        {
            return builder.AddProperty("background", backgroundColor.ToRgbaStr(), when);
        }

        public static ICSSBuilder AddThickness(this ICSSBuilder builder, string property, Thickness thickness, bool when = true)
        {
            return when
                ? builder.AddProperty(property, CSSHelper.CreateThicknessValue(thickness))
                : builder;
        }

        public static ICSSBuilder AddPadding(this ICSSBuilder builder, Thickness thickness, bool when = true)
        {
            return AddThickness(builder, "padding", thickness, when);
        }

        public static ICSSBuilder AddMargin(this ICSSBuilder builder, Thickness thickness, bool when = true)
        {
            return AddThickness(builder, "margin", thickness, when);
        }

        public static ICSSBuilder AddBorderRadius(this ICSSBuilder builder, Thickness thickness, bool when = true)
        {
            return AddThickness(builder, "border-radius", thickness, when);
        }

        public static ICSSBuilder AddBorderWidth(this ICSSBuilder builder, Thickness thickness, bool when = true)
        {
            return AddThickness(builder, "border-width", thickness, when);
        }

        public static ICSSBuilder AddBorder(this ICSSBuilder builder, Length thickness, Color color, bool when = true)
        {
            return when
                ? builder.AddProperty("border", CSSHelper.CreateBorderValue(thickness, color))
                : builder;
        }

        public static ICSSBuilder AddBorderColor(this ICSSBuilder builder, Color color, bool when = true)
        {
            return builder.AddProperty("border-color", color.ToRgbaStr(), when);
        }

        public static ICSSBuilder AddBorderStyle(this ICSSBuilder builder, string? style, bool when = true)
        {
            return builder.AddProperty("border-style", style, when);
        }

        public static ICSSBuilder AddShadow(this ICSSBuilder builder, Shadow shadow, bool when = true)
        {
            return when
                ? builder.AddProperty("box-shadow", CSSHelper.CreateBoxShadowValue(shadow))
                : builder;
        }

        public static ICSSBuilder AddShadow(this ICSSBuilder builder, Shadow[] shadow, bool when = true)
        {
            return when
                ? builder.AddProperty("box-shadow", CSSHelper.CreateBoxShadow(shadow))
                : builder;
        }

        public static ICSSBuilder AddFlexBasis(this ICSSBuilder builder, Length value, bool when = true)
        {
            return builder.AddProperty("flex-basis", value.ToString(), when);
        }

        public static ICSSBuilder AddFlexGrow(this ICSSBuilder builder, int value, bool when = true)
        {
            return (when && value >= 0)
                ? builder.AddProperty("flex-grow", value.ToString())
                : builder;
        }

        public static ICSSBuilder AddFlexShrink(this ICSSBuilder builder, int value, bool when = true)
        {
            return (when && value >= 0)
                ? builder.AddProperty("flex-shrink", value.ToString())
                : builder;
        }

        public static ICSSBuilder AddFlex(this ICSSBuilder builder, Length basis, int grow, int shrink, bool when = true)
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
    }
}