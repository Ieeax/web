using System;

namespace Leeax.Web
{
    public readonly struct Thickness : IEquatable<Thickness>
    {
        private readonly bool _initialized;

        public Thickness(Dimension all)
            : this(all, all, all, all)
        {
        }

        public Thickness(Dimension vertical, Dimension horizontal)
            : this(vertical, horizontal, vertical, horizontal)
        {
        }

        public Thickness(Dimension top, Dimension right, Dimension bottom, Dimension left)
        {
            _initialized = true;
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        public bool Equals(Thickness other)
        {
            return Top.Equals(other.Top) 
                && Right.Equals(other.Right)
                && Bottom.Equals(other.Bottom)  
                && Left.Equals(other.Left);
        }

        public override bool Equals(object? obj)
            => obj is Thickness other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(Top, Right, Bottom, Left);

        public Dimension Top { get; }

        public Dimension Right { get; }

        public Dimension Bottom { get; }

        public Dimension Left { get; }

        public bool IsEmpty => !_initialized;

        public static Thickness Empty { get; } = new Thickness();

        public static bool operator ==(Thickness first, Thickness second)
            => first.Equals(second);

        public static bool operator !=(Thickness first, Thickness second)
            => !first.Equals(second);

        public static implicit operator Thickness(Dimension t) => new Thickness(t);
        public static implicit operator Thickness((Dimension, Dimension) t) => new Thickness(t.Item1, t.Item2);
        public static implicit operator Thickness((Dimension, Dimension, Dimension, Dimension) t) => new Thickness(t.Item1, t.Item2, t.Item3, t.Item4);
    }
}