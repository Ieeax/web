using System.Drawing;

namespace Leeax.Web
{
    public readonly struct Shadow
    {
        public Shadow(Length offsetX, Length offsetY, Length blurRadius, Length spreadRadius, Color color)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
            BlurRadius = blurRadius;
            SpreadRadius = spreadRadius;
            Color = color;
        }

        public Shadow(Length offsetX, Length offsetY, Length blurRadius, Color color)
            : this(offsetX, offsetY, blurRadius, 0, color)
        {
        }

        public Shadow(Length offsetX, Length offsetY, Length blurRadius)
            : this(offsetX, offsetY, blurRadius, Color.Black)
        {
        }

        public Shadow(Length blurRadius, Length spreadRadius, Color color)
            : this(0, 0, blurRadius, spreadRadius, color)
        {
        }

        public Shadow(Length blurRadius, Length spreadRadius)
            : this(0, 0, blurRadius, spreadRadius, Color.Black)
        {
        }

        public static bool IsSet(Shadow value)
        {
            return !(value.OffsetX == 0
                && value.OffsetY == 0
                && value.BlurRadius == 0
                && value.SpreadRadius == 0);
        }

        public Length OffsetX { get; }

        public Length OffsetY { get; }

        public Length BlurRadius { get; }

        public Length SpreadRadius { get; }

        public Color Color { get; }
    }
}