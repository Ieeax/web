using System;
using Leeax.Web.Builders.Internal;

namespace Leeax.Web.Builders
{
    public static class AttributeSetBuilderExtensions
    {
        public static AttributeSetBuilder AddClassAttribute(this AttributeSetBuilder builder, params string[] classList)
        {
            if (classList.Length == 0)
            {
                return builder;
            } 

            return builder.AddAttribute("class", x => (x == null ? null : x + " ") + string.Join(" ", classList));
        }

        public static AttributeSetBuilder AddClassAttribute(this AttributeSetBuilder builder, Action<IClassBuilder> builderFactory)
        {
            builderFactory.ThrowIfNull();

            var classBuilder = ClassBuilder.Create();

            builderFactory.Invoke(classBuilder);

            return builder.AddAttribute("class", x => (x == null ? null : x + " ") + classBuilder.ToString());
        }

        public static AttributeSetBuilder AddStyleAttribute(this AttributeSetBuilder builder, Action<ICssBuilder> builderFactory)
        {
            builderFactory.ThrowIfNull();

            var cssBuilder = CssBuilder.Create();

            builderFactory.Invoke(cssBuilder);

            return builder.AddAttribute("style", x => x + cssBuilder.ToString());
        }

        public static AttributeSetBuilder AddDataAttribute(this AttributeSetBuilder builder, string name, object? value)
        {
            name.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty. \"data\"-attributes require a name.", nameof(name));
            }

            return builder.AddAttribute("data-" + name, value?.ToString());
        }

        public static AttributeSetBuilder AddAriaAttribute(this AttributeSetBuilder builder, string name, object? value)
        {
            name.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty. \"aria\"-attributes require a name.", nameof(name));
            }

            return builder.AddAttribute("aria-" + name, value?.ToString());
        }
    }
}