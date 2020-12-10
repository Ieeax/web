using System;

namespace Leeax.Web.Builders
{
    public interface IClassBuilder
    {
        /// <summary>
        /// Merges the specified builder to the current one, except <paramref name="when"/> is <see langword="false"/> or <paramref name="value"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="builder">The builder to merch.</param>
        /// <param name="when">Determines whether the specified builder should be merged to the current builder. Especially useful when chaining multiple methods.</param>
        IClassBuilder Merge(IClassBuilder? builder, bool when = true);

        /// <summary>
        /// Merges the specified builder from the factory to the current one, except <paramref name="when"/> is <see langword="false"/> or the <paramref name="builderFactory"/> (result) is <see langword="null"/>.
        /// </summary>
        /// <param name="builderFactory">The factory method which should return the builder to merch. Will only be executed if <paramref name="when"/> is <see langword="true"/>.</param>
        /// <param name="when">Determines whether the specified builder should be merged to the current builder. Especially useful when chaining multiple methods.</param>
        IClassBuilder Merge(Func<IClassBuilder?>? builderFactory, bool when);

        /// <summary>
        /// Adds the specific class to the builder, except <paramref name="when"/> is <see langword="false"/> or <paramref name="value"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The class name.</param>
        /// <param name="when">Determines whether the specified class should be added to the builder. Especially useful when chaining multiple methods.</param>
        IClassBuilder Add(string? value, bool when = true);

        /// <summary>
        /// Adds the specific class from the factory to the builder, except <paramref name="when"/> is <see langword="false"/> or the <paramref name="valueFactory"/> (result) is <see langword="null"/>.
        /// </summary>
        /// <param name="valueFactory">The factory method which should return the class name. Will only be executed if <paramref name="when"/> is <see langword="true"/>.</param>
        /// <param name="when">Determines whether the specified class should be added to the builder. Especially useful when chaining multiple methods.</param>
        IClassBuilder Add(Func<string?>? valueFactory, bool when);

        /// <summary>
        /// Adds the specific class, based on the <paramref name="when"/> condition, to the builder. If the value is <see langword="null"/> no class will be added.
        /// </summary>
        /// <param name="valueWhenTrue">The class name which gets applied if the condition is <see langword="true"/>.</param>
        /// <param name="valueWhenFalse">The class name which gets applied if the condition is <see langword="false"/>.</param>
        /// <param name="when">Determines whether the first or second class should be added to the builder. Especially useful when chaining multiple methods.</param>
        IClassBuilder Add(string? valueWhenTrue, string? valueWhenFalse, bool when);

        /// <summary>
        /// Adds the specified classes to the builder, except <paramref name="values"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="values">The class names.</param>
        IClassBuilder AddMultiple(params string[]? values);

        /// <summary>
        /// Adds the specified classes to the builder, except <paramref name="when"/> is <see langword="false"/> or <paramref name="values"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="values">The class names.</param>
        /// <param name="when">Determines whether the specified classes should be added to the builder. Especially useful when chaining multiple methods.</param>
        IClassBuilder AddMultiple(string[]? values, bool when);

        /// <summary>
        /// Adds the specified classes to the builder, except <paramref name="when"/> is <see langword="false"/> or the <paramref name="valueFactory"/> (result) is <see langword="null"/>.
        /// </summary>
        /// <param name="valuesFactory">The factory method which should return the class names. Will only be executed if <paramref name="when"/> is <see langword="true"/>.</param>
        /// <param name="when">Determines whether the specified classes should be added to the builder. Especially useful when chaining multiple methods.</param>
        IClassBuilder AddMultiple(Func<string[]?>? valuesFactory, bool when);

        /// <summary>
        /// Clears the builder, all previous added classes will be removed.
        /// </summary>
        IClassBuilder Clear();

        /// <summary>
        /// Gets the string.
        /// </summary>
        string? Build();
    }
}