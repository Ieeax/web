using System;
using System.Collections.Generic;
using Leeax.Web.Builders.Internal;

namespace Leeax.Web.Builders
{
    public class AttributeSetBuilder
    {
        private Dictionary<string, object?>? _attributes;

        private AttributeSetBuilder()
        {
        }

        public static AttributeSetBuilder Create()
        {
            return new AttributeSetBuilder();
        }

        public static AttributeSetBuilder Merge(params AttributeSetBuilder[] collection)
        {
            collection.ThrowIfNull();

            var builder = AttributeSetBuilder.Create();

            foreach (var curBuilder in collection)
            {
                if (curBuilder == null)
                {
                    continue;
                }

                builder.Merge(curBuilder);
            }

            return builder;
        }

        public AttributeSetBuilder AddAttribute(string key, Func<object?> valueFactory, bool when = true, bool keyAsValueWhenNull = true)
        {
            key.ThrowIfNull();
            valueFactory.ThrowIfNull();

            if (when)
            {
                return AddAttribute(key, valueFactory.Invoke(), true, keyAsValueWhenNull);
            }

            return this;
        }

        public AttributeSetBuilder AddAttribute(string key, Func<object?, object?> valueMergeFactory, bool when = true, bool keyAsValueWhenNull = true)
        {
            key.ThrowIfNull();
            valueMergeFactory.ThrowIfNull();

            if (!when)
            {
                return this;
            }

            object? existingValue = null;

            // Try to get existing value with given key
            if (_attributes != null)
            {
                _attributes.TryGetValue(key, out existingValue);
            }

            return AddAttribute(
                key,
                valueMergeFactory.Invoke(existingValue), // Invoke factory with the existing value
                true, 
                keyAsValueWhenNull);
        }

        public AttributeSetBuilder AddAttribute(string key, object? value, bool when = true, bool keyAsValueWhenNull = true)
        {
            key.ThrowIfNull();

            if (!when)
            {
                return this;
            }

            if (value == null)
            {
                if (!keyAsValueWhenNull)
                {
                    return this;
                }

                // If value is null, set the value to the key
                value = key;
            }

            if (_attributes == null)
            {
                _attributes = new Dictionary<string, object?>();
            }

            _attributes[key] = value;

            return this;
        }

        public AttributeSetBuilder RemoveAttribute(string key)
        {
            key.ThrowIfNull();

            if (_attributes != null)
            {
                _attributes.Remove(key);
            }

            return this;
        }

        public AttributeSetBuilder Merge(AttributeSetBuilder? builder, bool when = true)
        {
            if (!when 
                || builder == null)
            {
                return this;
            }

            // Merge all attributes
            if (builder._attributes != null)
            {
                if (_attributes == null)
                {
                    _attributes = new Dictionary<string, object?>();
                }

                foreach (var curAttribute in builder._attributes)
                {
                    _attributes[curAttribute.Key] = curAttribute.Value;
                }
            }

            return this;
        }

        public AttributeSetBuilder Merge(IEnumerable<KeyValuePair<string, object?>>? attributes, bool when = true, bool keyAsValueWhenNull = true)
        {
            if (!when
                || attributes == null)
            {
                return this;
            }

            foreach (var curAttribute in attributes)
            {
                AddAttribute(curAttribute.Key, curAttribute.Value, true, keyAsValueWhenNull);
            }

            return this;
        }

        public AttributeSet? Build()
        {
            return _attributes != null
                ? AttributeSet.Create(_attributes)
                : null;
        }
    }
}