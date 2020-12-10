using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Abstractions
{
    public class BackwardElementReference
    {
        private ElementReference _current;

        //public event Action<ElementReference> Changed;

        public BackwardElementReference()
        {
        }

        public BackwardElementReference(ElementReference reference)
        {
            _current = reference;
        }

        public ElementReference Current
        { 
            get => _current; 
            set
            {
                _current = value;
                //Changed?.Invoke(value);
            }
        }

        public bool HasValue => _current.Id != null;
    }
}