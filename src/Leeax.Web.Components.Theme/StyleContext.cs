namespace Leeax.Web.Components.Theme
{
    public class StyleContext
    {
        private StyleContext? _ancestor;
        private StyleBase? _current;

        public StyleContext()
        {
        }

        public StyleContext(StyleBase? current, StyleContext? previous)
        {
            _current = current;
            _ancestor = previous;
        }

        internal void SetStyle(StyleBase? value)
        {
            _current = value;
        }

        internal void SetAncestor(StyleContext? value)
        {
            _ancestor = value;
        }

        /// <summary>
        /// Gets the value which is associated with the specified key.
        /// </summary>
        public bool TryGetValue<T>(string key, out T? value)
        {
            if (_current != null
                && _current.TryGetValue(key, out var valusAsObject))
            {
                if (valusAsObject is T a)
                {
                    value = a;
                    return true;
                }

                if (_ancestor == null)
                {
                    value = default;
                    return false;
                }

                if (valusAsObject is KeyAlias b)
                {
                    return _ancestor.TryGetValue(b.Value, out value);
                }

                value = default;
                return false;
            }

            if (_ancestor == null)
            {
                value = default;
                return false;
            }

            return _ancestor.TryGetValue(key, out value);
        }

        /// <summary>
        /// Tries to get the value associated with the specified key. If not found the <paramref name="defaultValue"/> will be returned.
        /// </summary>
        public T? GetValueOrDefault<T>(string key, T? defaultValue = default)
        {
            return TryGetValue<T>(key, out var value)
                ? value
                : defaultValue;
        }

        /// <summary>
        /// Tries to get the value associated with the specified key or fallback-key. If not found the <paramref name="defaultValue"/> will be returned.
        /// </summary>
        public T? GetValueOrDefault<T>(string key, string fallbackKey, T? defaultValue = default)
        {
            if (_current != null
                && (_current.TryGetValue(key, out var valueAsObject)
                    || _current.TryGetValue(fallbackKey, out valueAsObject)))
            {
                if (valueAsObject is T a)
                {
                    return a;
                }

                if (_ancestor == null)
                {
                    return defaultValue;
                }

                if (valueAsObject is KeyAlias b)
                {
                    // TODO: We may want to get the fallback key if we don't find the initial alias
                    return _ancestor.GetValueOrDefault(b.Value, defaultValue);
                }

                return defaultValue;
            }

            if (_ancestor == null)
            {
                return defaultValue;
            }

            return _ancestor.GetValueOrDefault(key, fallbackKey, defaultValue);
        }

        public StyleBase? Style => _current;
    }
}