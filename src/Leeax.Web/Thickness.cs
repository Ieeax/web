namespace Leeax.Web
{
    public readonly struct Thickness
    {
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
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        public static bool IsSet(Thickness value)
        {
            return value.Left != 0
                || value.Top != 0
                || value.Right != 0
                || value.Bottom != 0;
        }

        public bool IsEmpty => !IsSet(this);

        public Length Top { get; }

        public Length Right { get; }

        public Length Bottom { get; }

        public Length Left { get; }

        public static implicit operator Thickness(int t) => new Thickness(t);
        public static implicit operator Thickness((int, int) t) => new Thickness(t.Item1, t.Item2);
        public static implicit operator Thickness((int, int, int, int) t) => new Thickness(t.Item1, t.Item2, t.Item3, t.Item4);

        public static implicit operator Thickness(Length t) => new Thickness(t);
        public static implicit operator Thickness((Length, Length) t) => new Thickness(t.Item1, t.Item2);
        public static implicit operator Thickness((Length, Length, Length, Length) t) => new Thickness(t.Item1, t.Item2, t.Item3, t.Item4);
    }
}