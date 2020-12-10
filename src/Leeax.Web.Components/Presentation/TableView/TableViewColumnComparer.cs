using Leeax.Web.Components.Reflection;
using System.Collections.Generic;

namespace Leeax.Web.Components.Presentation
{
    public class TableViewColumnComparer : IComparer<object?>
    {
        private readonly IComparer<object> _defaultComparer;
        private readonly Binding _binding;
        private readonly IConverter? _converter;

        public TableViewColumnComparer(Binding binding)
            : this(binding, null)
        {
        }

        public TableViewColumnComparer(Binding binding, IConverter? converter)
        {
            _binding = binding;
            _converter = converter;
            _defaultComparer = Comparer<object>.Default;
        }

        public int Compare(object? x, object? y)
        {
            ReflectionHelper.TryGetPropertyValue(x, _binding, out var first);
            ReflectionHelper.TryGetPropertyValue(y, _binding, out var second);

            if (_converter == null)
            {
                return _defaultComparer.Compare(first, second);
            }

            return _defaultComparer.Compare(
                _converter.ConvertTypeSafe<string>(first),
                _converter.ConvertTypeSafe<string>(second));
        }
    }
}