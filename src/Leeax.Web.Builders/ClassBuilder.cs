using System;

namespace Leeax.Web.Builders
{
    public class ClassBuilder : IClassBuilder
    {
        private string? _str;

        private ClassBuilder()
        {
        }

        #region Factory methods to create builder
        public static ClassBuilder Create()
        {
            return new ClassBuilder();
        }

        public static ClassBuilder Create(params string?[]? classes)
        {
            var builder = new ClassBuilder();

            if (classes != null)
            {
                foreach (var curClass in classes)
                {
                    if (curClass != null)
                    {
                        builder._str += (curClass + " ");
                    }
                }
            }

            return builder;
        }

        /// <summary>
        /// Merges the passed <see cref="ClassBuilder"/>'s and returns a new one.
        /// </summary>
        /// <param name="collection">The builders to merge.</param>
        public static ClassBuilder Merge(params ClassBuilder?[]? collection)
        {
            var builder = new ClassBuilder();

            if (collection != null)
            {
                foreach (var curBuilder in collection)
                {
                    if (curBuilder != null)
                    {
                        var str = curBuilder?.Build();
                        if (str != null)
                        {
                            builder._str += (str + " ");
                        }
                    }
                }
            }

            return builder;
        }
        #endregion

        /// <inheritdoc/>
        public IClassBuilder Merge(IClassBuilder? builder, bool when = true)
        {
            if (builder != null
                && when)
            {
                _str += (builder.ToString() + " ");
            }

            return this;
        }

        /// <inheritdoc/>
        public IClassBuilder Merge(Func<IClassBuilder?>? builderFactory, bool when)
        {
            if (builderFactory != null
                && when)
            {
                var builder = builderFactory();

                if (builder != null)
                {
                    _str += (builder.ToString() + " ");
                }
            }

            return this;
        }

        /// <inheritdoc/>
        public IClassBuilder Add(string? value, bool when = true)
        {
            if (value != null
                && when)
            {
                _str += (value + " ");
            }

            return this;
        }

        /// <inheritdoc/>
        public IClassBuilder Add(Func<string?>? valueFactory, bool when)
        {
            if (valueFactory != null
                && when)
            {
                var value = valueFactory();

                if (value != null)
                {
                    _str += (value + " ");
                }
            }

            return this;
        }

        /// <inheritdoc/>
        public IClassBuilder Add(string? valueWhenTrue, string? valueWhenFalse, bool when)
        {
            var value = when ? valueWhenTrue : valueWhenFalse;
            if (value != null)
            {
                _str += (value + " ");
            }

            return this;
        }

        /// <inheritdoc/>
        public IClassBuilder AddMultiple(params string[]? values)
        {
            if (values != null
                && values.Length > 0)
            {
                _str += (string.Join(" ", values) + " ");
            }

            return this;
        }

        /// <inheritdoc/>
        public IClassBuilder AddMultiple(string[]? values, bool when)
        {
            if (values != null
                && values.Length > 0
                && when)
            {
                _str += (string.Join(" ", values) + " ");
            }

            return this;
        }

        /// <inheritdoc/>
        public IClassBuilder AddMultiple(Func<string[]?>? valuesFactory, bool when)
        {
            if (valuesFactory != null
                && when)
            {
                var values = valuesFactory();

                if (values != null)
                {
                    _str += (string.Join(" ", values) + " ");
                }
            }

            return this;
        }

        /// <inheritdoc/>
        public IClassBuilder Clear()
        {
            _str = null;

            return this;
        }

        /// <inheritdoc/>
        public string? Build() => ToString();

        public override string? ToString()
        {
            var value = _str?.TrimEnd(' ');

            // Ensure to return null if empty, so the attribute dont even get set
            return string.IsNullOrEmpty(value)
                ? null
                : value;
        }
    }
}