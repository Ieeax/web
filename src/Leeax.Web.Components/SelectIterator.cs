using Leeax.Web.Components.Input;
using System.Collections;
using System.Collections.Generic;

namespace Leeax.Web.Components
{
    public class SelectIterator<TSource, TResult> : IEnumerable<(TSource Source, TResult? Result, int Index)>
    {
        private readonly IEnumerable<TSource>? _enumerable;
        private readonly IConverter? _converter;
        private readonly bool _castWithoutConverter;

        public SelectIterator(IEnumerable<TSource>? enumerable, IConverter? converter)
        {
            _enumerable = enumerable;
            _converter = converter;
            _castWithoutConverter = converter == null && typeof(TResult).IsAssignableFrom(typeof(TSource));
        }

        public IEnumerator<(TSource Source, TResult? Result, int Index)> GetEnumerator()
        {
            if (_enumerable == null)
            {
                yield break;
            }

            var index = 0;
            foreach (var curItem in _enumerable)
            {
                yield return (
                    curItem,
                    _castWithoutConverter
                        ? curItem == null ? default : (TResult)(object)curItem
                        : _converter == null ? default : _converter.ConvertTypeSafe<TResult>(curItem), 
                    index++
                );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}