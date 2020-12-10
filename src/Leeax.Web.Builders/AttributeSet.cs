using System;
using System.Collections;
using System.Collections.Generic;
using Leeax.Web.Builders.Internal;

namespace Leeax.Web.Builders
{
    public class AttributeSet : IEnumerable<KeyValuePair<string, object?>>
    {
        private readonly IEnumerable<KeyValuePair<string, object?>> _enumerable;

        private AttributeSet(IEnumerable<KeyValuePair<string, object?>> collection)
        {
            _enumerable = collection;
        }

        public static AttributeSet Create(IEnumerable<KeyValuePair<string, object?>> collection)
        {
            collection.ThrowIfNull();

            return new AttributeSet(collection);
        }

        public static AttributeSet? Create(Action<AttributeSetBuilder> builderFactory)
        {
            builderFactory.ThrowIfNull();

            var builder = CreateBuilder();
            builderFactory.Invoke(builder);

            return builder.Build();
        }

        public static AttributeSet? Merge(params AttributeSet[] collection)
        {
            collection.ThrowIfNull();

            var resultSet = AttributeSetBuilder.Create();

            foreach (var curAttrSet in collection)
            {
                if (curAttrSet == null)
                {
                    continue;
                }

                resultSet.Merge(curAttrSet);
            }

            return resultSet.Build();
        }

        public static AttributeSetBuilder CreateBuilder()
        {
            return AttributeSetBuilder.Create();
        }

        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator() => _enumerable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}