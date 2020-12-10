using Leeax.Web.Components.Modals;
using System;
using System.Collections.Generic;

namespace Leeax.Web.Components.Configuration
{
    public class ModalOptions
    {
        private readonly Dictionary<Type, Type> _modelComponentMapping;

        public ModalOptions()
        {
            _modelComponentMapping = new Dictionary<Type, Type>();
        }

        public void AddComponent<TModel, TComponent>()
            where TModel : INotifyClosed
            where TComponent : IModelComponent<TModel>
        {
            var modelType = typeof(TModel);

            if (_modelComponentMapping.ContainsKey(modelType))
            {
                throw new ApplicationException($"Another component with model '{modelType.Name}' as key is already registered.");
            }

            _modelComponentMapping.Add(
                modelType,
                typeof(TComponent));
        }

        public IReadOnlyDictionary<Type, Type> Mapping => _modelComponentMapping;
    }
}