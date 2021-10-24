namespace Leeax.Web
{
    public readonly struct Thickness
    {
        private readonly bool _initialized;

        public Thickness(Length all)
            : this(all, all, all, all)
        {
        }

        public Thickness(Length vertical, Length horizontal)
            : this(vertical, horizontal, vertical, horizontal)
        {
        }

        public Thickness(Length top, Length right, Length bottom, Length left)
        {
            _initialized = true;

            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        public override bool Equals(object? obj)
            => obj is Thickness thickness ? this == thickness : false;

        public override int GetHashCode()
            => Top.GetHashCode() ^ Right.GetHashCode() ^ Bottom.GetHashCode() ^ Left.GetHashCode();

        public Length Top { get; }

        public Length Right { get; }

        public Length Bottom { get; }

        public Length Left { get; }

        public bool IsEmpty => !_initialized;

        public static Thickness Empty { get; } = new Thickness();

        public static bool operator ==(Thickness first, Thickness second)
            => first.Top == second.Top && first.Bottom == second.Bottom
            && first.Left == second.Left && first.Right == second.Right;

        public static bool operator !=(Thickness first, Thickness second)
            => first.Top != second.Top || first.Bottom != second.Bottom
            || first.Left != second.Left || first.Right != second.Right;

        public static implicit operator Thickness(int t) => new Thickness(t);
        public static implicit operator Thickness((int, int) t) => new Thickness(t.Item1, t.Item2);
        public static implicit operator Thickness((int, int, int, int) t) => new Thickness(t.Item1, t.Item2, t.Item3, t.Item4);

        public static implicit operator Thickness(Length t) => new Thickness(t);
        public static implicit operator Thickness((Length, Length) t) => new Thickness(t.Item1, t.Item2);
        public static implicit operator Thickness((Length, Length, Length, Length) t) => new Thickness(t.Item1, t.Item2, t.Item3, t.Item4);
    }
}