using System;
using System.Globalization;
using Leeax.Web.Internal;

namespace Leeax.Web
{
    public readonly struct Dimension : IEquatable<Dimension>
    {
        private readonly bool _initialized;

        public Dimension(double value)
            : this(value, Unit.Pixel)
        {
        }

        public Dimension(double value, Unit unit)
        {
            _initialized = true;

            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Converts the specified string representation of a number with unit to its <see cref="Dimension"/> equivalent.
        /// </summary>
        /// <param name="value">The number with unit to parse.</param>
        /// <exception cref="FormatException"></exception>
        public static Dimension Parse(string value)
        {
            value.ThrowIfNull();
            
            return TryParse(value, out var dimension) 
                ? dimension 
                : throw new FormatException("The value has an invalid format.");
        }
        
        /// <summary>
        /// Converts the specified string representation of a number with unit to its <see cref="Dimension"/> equivalent and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">The number with unit to parse.</param>
        /// <param name="result">The <see cref="Dimension"/> equivalent of the specified value.</param>
        public static bool TryParse(string? value, out Dimension result)
        {
            result = default;
            
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            Unit unit;
            int startIndexUnit = -1;
            
            if (value[^1] == '%')
            {
                unit = Unit.Percent;
                startIndexUnit = value.Length - 1;
            }
            else
            {
                for (int i = value.Length - 1; i >= 0; i--)
                {
                    if (!char.IsLetter(value[i]))
                    {
                        startIndexUnit = i + 1;
                        break;
                    }
                }
                
                if (startIndexUnit >= 0
                    && startIndexUnit < value.Length)
                {
                    if (!UnitHelper.TryParse(value[startIndexUnit..], out unit))
                    {
                        return false;
                    }
                }
                else
                {
                    unit = Unit.Pixel;
                    startIndexUnit = value.Length;
                }
            }
            
            if (!double.TryParse(value[..startIndexUnit], NumberStyles.Float, CultureInfo.InvariantCulture, out var number))
            {
                return false;
            }

            result = new Dimension(number, unit);
            return true;
        }
        
        public override string ToString()
            => Value.ToString(CultureInfo.InvariantCulture) + UnitHelper.Format(Unit);

        public bool Equals(Dimension other)
            => Value.Equals(other.Value) && Unit == other.Unit;

        public override bool Equals(object? obj)
            => obj is Dimension other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(Value, Unit);

        public double Value { get; }

        public Unit Unit { get; }

        public bool IsEmpty => !_initialized;

        public static Dimension Empty { get; } = new Dimension();

        public static bool operator <(Dimension first, Dimension second)
            => first.Value < second.Value;

        public static bool operator >(Dimension first, Dimension second)
            => first.Value > second.Value;

        public static bool operator ==(Dimension first, Dimension second)
            => first.Equals(second);

        public static bool operator !=(Dimension first, Dimension second)
            => !first.Equals(second);

        public static bool operator <(Dimension first, double second)
            => first.Value < second;

        public static bool operator >(Dimension first, double second)
            => first.Value > second;

        public static bool operator ==(Dimension first, double second)
            => first.Value.Equals(second);

        public static bool operator !=(Dimension first, double second)
            => !first.Value.Equals(second);

        public static bool operator <(double first, Dimension second)
            => first < second.Value;

        public static bool operator >(double first, Dimension second)
            => first > second.Value;

        public static bool operator ==(double first, Dimension second)
            => first.Equals(second.Value);

        public static bool operator !=(double first, Dimension second)
            => !first.Equals(second.Value);

        public static implicit operator Dimension(string value) => Dimension.Parse(value);
        public static implicit operator Dimension(double value) => new Dimension(value);
        public static implicit operator Dimension((double, Unit) value) => new Dimension(value.Item1, value.Item2);
    }
}