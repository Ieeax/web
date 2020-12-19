using System;

namespace Leeax.Web.Builders
{
    public class CssBuilder : ICssBuilder
    {
        private string? _str;

        private CssBuilder()
        {
        }

        #region Factory methods to create builder
        public static CssBuilder Create()
        {
            return new CssBuilder();
        }

        public static CssBuilder Create(string? value)
        {
            var builder = new CssBuilder();

            if (value != null)
            {
                builder._str = value.TrimEnd(' ');
            }
            
            return builder;
        }

        /// <summary>
        /// Merges the passed <see cref="CssBuilder"/>'s and returns a new one.
        /// </summary>
        /// <param name="collection">The builders to merge.</param>
        public static CssBuilder Merge(params CssBuilder[] collection)
        {
            var builder = new CssBuilder();

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
        public ICssBuilder AddProperty(string property, string? value, bool when = true)
        {
            if (value != null
                && when)
            {
                _str += property + ":" + ConvertValue(value) + ";";
            }

            return this;
        }

        /// <inheritdoc/>
        public ICssBuilder AddProperty(string property, Func<string?>? valueFactory, bool when = true)
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
        public ICssBuilder Merge(ICssBuilder? builder, bool when = true)
        {
            _str += when
                ? builder?.Build()
                : string.Empty;

            return this;
        }

        /// <inheritdoc/>
        public ICssBuilder Merge(Func<ICssBuilder?>? builderFactory, bool when = true)
        {
            _str += when
                ? builderFactory?.Invoke()?.Build()
                : string.Empty;

            return this;
        }

        /// <inheritdoc/>
        public ICssBuilder Clear()
        {
            _str = null;

            return this;
        }

        /// <inheritdoc/>
        public string? Build() => ToString();

        public override string? ToString() => _str;
    }
}
