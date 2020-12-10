using System;

namespace Leeax.Web.Builders
{
    public interface ICSSBuilder
    {
        /// <summary>
        /// Adds the specific property to the builder, except <paramref name="when"/> is <see langword="false"/> or <paramref name="value"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <param name="value">The property value.</param>
        /// <param name="when">Determines whether the specified property should be added to the builder. Especially useful when chaining multiple methods.</param>
        ICSSBuilder AddProperty(string property, string? value, bool when = true);

        /// <summary>
        /// Adds the specific property with the value from the factory to the builder, except <paramref name="when"/> is <see langword="false"/> or the <paramref name="valueFactory"/> (result) is <see langword="null"/>.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <param name="valueFactory">The factory method which should return the property value. Will only be executed if <paramref name="when"/> is <see langword="true"/>.</param>
        /// <param name="when">Determines whether the specified property should be added to the builder. Especially useful when chaining multiple methods.</param>
        ICSSBuilder AddProperty(string property, Func<string?>? valueFactory, bool when = true);

        /// <summary>
        /// Merges the specified builder to the current one, except <paramref name="when"/> is <see langword="false"/> or <paramref name="value"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="builder">The builder to merch.</param>
        /// <param name="when">Determines whether the specified builder should be merged to the current builder. Especially useful when chaining multiple methods.</param>
        ICSSBuilder Merge(ICSSBuilder? builder, bool when = true);

        /// <summary>
        /// Merges the specified builder from the factory to the current one, except <paramref name="when"/> is <see langword="false"/> or the <paramref name="builderFactory"/> (result) is <see langword="null"/>.
        /// </summary>
        /// <param name="builderFactory">The factory method which should return the builder to merch. Will only be executed if <paramref name="when"/> is <see langword="true"/>.</param>
        /// <param name="when">Determines whether the specified builder should be merged to the current builder. Especially useful when chaining multiple methods.</param>
        ICSSBuilder Merge(Func<ICSSBuilder?>? builderFactory, bool when = true);

        /// <summary>
        /// Clears the builder, all previous added properties/rules will be removed.
        /// </summary>
        ICSSBuilder Clear();

        /// <summary>
        /// Gets the string.
        /// </summary>
        string? Build();
    }
}