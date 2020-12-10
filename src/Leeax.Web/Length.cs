namespace Leeax.Web
{
    public readonly struct Length
    {
        public Length(double value)
            : this(value, Unit.Pixel)
        {
        }

        public Length(double value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        public static bool IsSet(Length value)
            => value.Value != 0;

        public override string ToString()
            => Value.ToString(System.Globalization.CultureInfo.InvariantCulture) + UnitHelper.ToString(Unit);

        public override bool Equals(object? obj)
        {
            return obj is Length length 
                ? this == length 
                : object.Equals(Value, obj);
        }

        public double Value { get; }

        public Unit Unit { get; }

        public static bool operator <(Length first, Length second)
            => first.Value < second.Value;

        public static bool operator >(Length first, Length second)
            => first.Value > second.Value;

        public static bool operator ==(Length first, Length second)
            => first.Value == second.Value && first.Unit == second.Unit;

        public static bool operator !=(Length first, Length second)
            => first.Value != second.Value || first.Unit != second.Unit;

        public static bool operator <(Length first, double second)
            => first.Value < second;

        public static bool operator >(Length first, double second)
            => first.Value > second;

        public static bool operator ==(Length first, double second)
            => first.Value == second;

        public static bool operator !=(Length first, double second)
            => first.Value != second;

        public static bool operator <(double first, Length second)
            => first < second.Value;

        public static bool operator >(double first, Length second)
            => first > second.Value;

        public static bool operator ==(double first, Length second)
            => first == second.Value;

        public static bool operator !=(double first, Length second)
            => first != second.Value;

        public static implicit operator Length(int value) => new Length(value);
        public static implicit operator Length(double value) => new Length(value);
        public static implicit operator Length((double, Unit) value) => new Length(value.Item1, value.Item2);
    }
}