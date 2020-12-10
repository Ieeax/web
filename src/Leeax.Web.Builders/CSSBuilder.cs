using System;

namespace Leeax.Web.Builders
{
    public class CSSBuilder : ICSSBuilder
    {
        private string? _str;

        private CSSBuilder()
        {
        }

        #region Factory methods to create builder
        public static CSSBuilder Create()
        {
            return new CSSBuilder();
        }

        public static CSSBuilder Create(string? value)
        {
            var builder = new CSSBuilder();

            if (value != null)
            {
                builder._str = value.TrimEnd(' ');
            }
            
            return builder;
        }

        /// <summary>
        /// Merges the passed <see cref="CSSBuilder"/>'s and returns a new one.
        /// </summary>
        /// <param name="collection">The builders to merge.</param>
        public static CSSBuilder Merge(params CSSBuilder[] collection)
        {
            var builder = new CSSBuilder();

            if (collection != null)
            {
                foreach (var curBuilder in collection)
                {
                    builder._str += curBuilder?.ToString();
                }
            }

            return builder;
        }
        #endregion

        /// <inheritdoc/>
        public ICSSBuilder AddProperty(string property, string? value, bool when = true)
        {
            if (value != null
                && when)
            {
                _str += property + ":" + ConvertValue(value) + ";";
            }

            return this;
        }

        /// <inheritdoc/>
        public ICSSBuilder AddProperty(string property, Func<string?>? valueFactory, bool when = true)
        {
            if (valueFactory != null
                && when)
            {
                var value = valueFactory();

                if (value != null)
                {
                    _str += ConvertValue(value) + " ";
                }
            }

            return this;
        }

        private string? ConvertValue(string? value)
        {
            if (value == string.Empty)
            {
                return "\"\"";
            }

            return value;
        }

        /// <inheritdoc/>
        public ICSSBuilder Merge(ICSSBuilder? builder, bool when = true)
        {
            _str += when
                ? builder?.Build()
                : string.Empty;

            return this;
        }

        /// <inheritdoc/>
        public ICSSBuilder Merge(Func<ICSSBuilder?>? builderFactory, bool when = true)
        {
            _str += when
                ? builderFactory?.Invoke()?.Build()
                : string.Empty;

            return this;
        }

        /// <inheritdoc/>
        public ICSSBuilder Clear()
        {
            _str = null;

            return this;
        }

        /// <inheritdoc/>
        public string? Build() => ToString();

        public override string? ToString() => _str;
    }
}
