using Leeax.Web.Internal;
using Leeax.Web.Components.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Modals
{
    public class ModalService : IModalService, IModalRenderService
    {
        private readonly IReadOnlyDictionary<Type, Type> _modelComponentMapping;
        private readonly IServiceProvider _serviceProvider;

        public event Action? StateChanged;

        public ModalService(IServiceProvider serviceProvider, ModalOptions options)
        {
            options.ThrowIfNull();

            _modelComponentMapping = options.Mapping;
            _serviceProvider = serviceProvider;
        }

        public Task ShowAsync<TModel>()
            where TModel : INotifyClosed
        {
            var instance = (TModel)ActivatorUtilities.CreateInstance(_serviceProvider, typeof(TModel));

            return ShowAsync(instance);
        }

        public Task ShowAsync<TModel>(Action<TModel>? configure)
            where TModel : INotifyClosed
        {
            var instance = (TModel)ActivatorUtilities.CreateInstance(_serviceProvider, typeof(TModel));

            configure?.Invoke(instance);

            return ShowAsync(instance);
        }

        public Task ShowAsync<TModel>(TModel model)
            where TModel : INotifyClosed
        {
            model.ThrowIfNull();

            var completionSource = new TaskCompletionSource<object?>();
            var modelType = typeof(TModel);

            if (_modelComponentMapping == null
                || !_modelComponentMapping.TryGetValue(modelType, out var componentType))
            {
                throw new ApplicationException($"No component for model '{modelType.Name}' found.");
            }

            ActiveModal = new ModalState(componentType, model);
            ActiveModal.Closed += OnClosed;

            StateChanged?.Invoke();

            return completionSource.Task;


            // Local event handler
            void OnClosed(ModalState state)
            {
                // Unsubscribe to all trigger and dispose toast
                state.Closed -= OnClosed;

                // Reset active dialog field
                ActiveModal.Dispose();
                ActiveModal = null;

                StateChanged?.Invoke();

                completionSource.SetResult(null);
            }
        }

        public ModalState? ActiveModal { get; private set; }
    }
}