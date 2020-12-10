namespace Leeax.Web.Components.Reflection
{
    public static class ReflectionHelper
    {
        // TODO: We may want to speed-up the reflection trough caching or smth ...
        public static bool TryGetPropertyValue(object? instance, Binding binding, out object? value)
        {
            value = null;

            if (instance == null
                || !binding.HasValue
                || string.IsNullOrWhiteSpace(binding.Path))
            {
                value = instance;
                return true;
            }

            var currentType = instance.GetType();

            foreach (string curPropertyName in binding.Path.Trim().Split('.'))
            {
                // Check for invalid property name
                if (string.IsNullOrWhiteSpace(curPropertyName))
                {
                    return false;
                }

                var property = currentType.GetProperty(curPropertyName);

                // Check if property was found
                if (property == null)
                {
                    return false;
                }

                instance = property.GetValue(instance, null);

                // Check if value has value
                if (instance == null)
                {
                    return false;
                }

                currentType = property.PropertyType;
            }

            value = instance;
            return true;
        }
    }
}