using Leeax.Web.Builders.Internal;
using System;

namespace Leeax.Web.Builders
{
    public static class StylesheetBuilderExtensions
    {
        /// <summary>
        /// Adds a new CSS definition with the specified selector to the stylesheet.
        /// </summary>
        /// <param name="selector">The CSS selector for this definition. Use the keyword <c>this</c> (in selector) to scope to the current component.</param>
        /// <param name="value">The raw CSS string.</param>
        public static StylesheetBuilder AddDefinition(this StylesheetBuilder builder, string selector, string? value)
        {
            selector.ThrowIfNull();

            builder.AddString(selector + "{" + value + "}");

            return builder;
        }

        /// <summary>
        /// Adds a new CSS definition with the specified selector to the stylesheet.
        /// </summary>
        /// <param name="selector">The CSS selector for this definition. Use the keyword <c>this</c> (in selector) to scope to the current component.</param>
        /// <param name="builderFactory">The factory which is used to create the CSS.</param>
        public static StylesheetBuilder AddDefinition(this StylesheetBuilder builder, string selector, Action<ICssBuilder> builderFactory)
        {
            selector.ThrowIfNull();
            builderFactory.ThrowIfNull();

            var cssBuilder = CssBuilder.Create();
            builderFactory.Invoke(cssBuilder);

            builder.AddString(selector + "{" + cssBuilder.Build() + "}");

            return builder;
        }

        /// <summary>
        /// Adds a new CSS definition with the specified selectors to the stylesheet.
        /// </summary>
        /// <param name="selectors">Array of CSS selectors for this definition. Use the keyword <c>this</c> (in selector) to scope to the current component.</param>
        /// <param name="builderFactory">The factory which is used to create the CSS.</param>
        public static StylesheetBuilder AddDefinition(this StylesheetBuilder builder, string[] selectors, Action<ICssBuilder> builderFactory)
        {
            selectors.ThrowIfNull();
            builderFactory.ThrowIfNull();

            if (selectors.Length == 0)
            {
                throw new ApplicationException($"Argument '{nameof(selectors)}' have to contain at least 1 item.");
            }

            builder.AddDefinition(string.Join(",", selectors), builderFactory);

            return builder;
        }
    }
}