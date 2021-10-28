using System;
using Leeax.Web.Builders.Internal;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Builders
{
    public static class RenderTreeBuilderExtensions
    {
        public static void AddClassAttribute(this RenderTreeBuilder builder, int sequence, params string[] classList)
        {
            if (classList.Length == 0)
            {
                return;
            } 

            builder.AddAttribute(sequence, "class", string.Join(" ", classList));
        }

        public static void AddClassAttribute(this RenderTreeBuilder builder, int sequence, Action<IClassBuilder> builderFactory)
        {
            builderFactory.ThrowIfNull();

            var classBuilder = ClassBuilder.Create();

            builderFactory.Invoke(classBuilder);

            builder.AddAttribute(sequence, "class", classBuilder.ToString());
        }

        public static void AddStyleAttribute(this RenderTreeBuilder builder, int sequence, Action<ICssBuilder> builderFactory)
        {
            builderFactory.ThrowIfNull();

            var cssBuilder = CssBuilder.Create();

            builderFactory.Invoke(cssBuilder);

            builder.AddAttribute(sequence, "style", cssBuilder.ToString());
        }

        public static void AddDataAttribute(this RenderTreeBuilder builder, int sequence, string name, object? value)
        {
            name.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty. \"data\"-attributes require a name.", nameof(name));
            }

            builder.AddAttribute(sequence, "data-" + name, value?.ToString());
        }

        public static void AddAriaAttribute(this RenderTreeBuilder builder, int sequence, string name, object? value)
        {
            name.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty. \"aria\"-attributes require a name.", nameof(name));
            }

            builder.AddAttribute(sequence, "aria-" + name, value?.ToString());
        }
    }
}