using System.Drawing;

namespace Leeax.Web
{
    public readonly struct Shadow
    {
        private readonly bool _initialized;

        public Shadow(Length offsetX, Length offsetY, Length blurRadius, Length spreadRadius, Color color)
        {
            _initialized = true;

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

        public override bool Equals(object? obj)
            => obj is Shadow shadow ? this == shadow : false;

        public override int GetHashCode()
            => OffsetX.GetHashCode() ^ OffsetY.GetHashCode() ^ BlurRadius.GetHashCode() ^ SpreadRadius.GetHashCode() ^ Color.GetHashCode();

        public Length OffsetX { get; }

        public Length OffsetY { get; }

        public Length BlurRadius { get; }

        public Length SpreadRadius { get; }

        public Color Color { get; }

        public bool IsEmpty => !_initialized;

        public static Shadow Empty { get; } = new Shadow();

        public static bool operator ==(Shadow first, Shadow second)
            => first.OffsetX == second.OffsetX && first.OffsetY == second.OffsetY
            && first.BlurRadius == second.BlurRadius && first.SpreadRadius == second.SpreadRadius
            && first.Color == second.Color;

        public static bool operator !=(Shadow first, Shadow second)
            => first.OffsetX != second.OffsetX || first.OffsetY != second.OffsetY
            || first.BlurRadius != second.BlurRadius || first.SpreadRadius != second.SpreadRadius
            || first.Color != second.Color;
    }
}