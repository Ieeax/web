using Leeax.Web.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ComponentsDemo.Components
{
    public class ComponentReflector
    {
        public ComponentReflector(Type componentType)
        {
            ComponentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
        }

        public static string GetFriendlyTypeName(Type type, bool includeNamespace = false)
        {
            if (type.IsGenericParameter)
            {
                return type.Name;
            }

            if (!type.IsGenericType)
            {
                return includeNamespace ? type.FullName : type.Name;
            }

            var builder = new StringBuilder();
            var name = type.Name;
            var index = name.IndexOf("`");

            if (includeNamespace)
            {
                builder.AppendFormat("{0}.{1}", type.Namespace, name.Substring(0, index));
            }
            else
            {
                builder.Append(name.Substring(0, index));
            }

            builder.Append('<');
            var first = true;

            foreach (var arg in type.GetGenericArguments())
            {
                if (!first)
                {
                    builder.Append(',');
                }
                builder.Append(GetFriendlyTypeName(arg, includeNamespace));
                first = false;
            }
            builder.Append('>');

            return builder.ToString();
        }

        public static IEnumerable<Type> GetSubComponentTypes(Type type)
        {
            var attributes = type.GetCustomAttributes<SubComponentAttribute>(true);

            foreach (var curAttr in attributes)
            {
                yield return curAttr.Type;
            }
        }

        public IEnumerable<CascadingParameterInfo> GetCascadingParameterInfos()
        {
            var properties = ComponentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var curProperty in properties)
            {
                var paramAttr = curProperty.GetCustomAttribute<CascadingParameterAttribute>(false);
                if (paramAttr == null)
                {
                    continue;
                }

                yield return new CascadingParameterInfo(
                    paramAttr.Name,
                    curProperty.PropertyType);
            }
        }

        public IEnumerable<ParameterInfo> GetParameterInfos()
        {
            var properties = ComponentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var curProperty in properties)
            {
                var paramAttr = curProperty.GetCustomAttribute<ParameterAttribute>(false);
                if (paramAttr == null)
                {
                    continue;
                }

                yield return new ParameterInfo(
                    curProperty.Name,
                    curProperty.PropertyType,
                    paramAttr.CaptureUnmatchedValues);
            }
        }

        public Type ComponentType { get; }
    }
}
