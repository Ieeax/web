using Leeax.Web.Internal;
using Leeax.Web.Components.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Leeax.Web.Components.Modals
{
    public class ToastService : IToastService, IToastRenderService
    {
        private readonly IReadOnlyDictionary<Type, Type> _modelComponentMapping;
        private readonly IList<ToastState> _activeToasts;
        private readonly IServiceProvider _serviceProvider;
        
        private ToastPosition _toastPosition;

        public event Action? StateChanged;

        public ToastService(IServiceProvider serviceProvider, ToastOptions options)
        {
            options.ThrowIfNull();

            _serviceProvider = serviceProvider;
            _activeToasts = new List<ToastState>();
            _modelComponentMapping = options.Mapping;

            ToastPosition = options.Position;
        }

        public void Show<TModel>(int displayTime = 5000)
        {
            var instance = (TModel)ActivatorUtilities.CreateInstance(_serviceProvider, typeof(TModel));

            Show(instance);
        }

        public void Show<TModel>(Action<TModel>? configure, int displayTime = 5000)
        {
            var instance = (TModel)ActivatorUtilities.CreateInstance(_serviceProvider, typeof(TModel));

            configure?.Invoke(instance);

            Show(instance);
        }

        public void Show<TModel>(TModel model, int displayTime = 5000)
        {
            model.ThrowIfNull();

            var modelType = typeof(TModel);

            if (!_modelComponentMapping.TryGetValue(modelType, out var componentType))
            {
                throw new ApplicationException($"No component for model '{modelType.Name}' found.");
            }

            var state = new ToastState(componentType, model!, displayTime);
            state.Closed += OnClosed;

            _activeToasts.Add(state);

            StateChanged?.Invoke();


            // Local event handler
            void OnClosed(ToastState state)
            {
                // Unsubscribe to events and dispose state
                state.Closed -= OnClosed;
                state.Dispose();

                // Remove from active toasts afterwards
                _activeToasts.Remove(state);

                StateChanged?.Invoke();
            }
        }

        public void ClearAll()
        {
            // Trigger close of all toasts, after transition the toasts closes automatically
            foreach (var curToast in _activeToasts)
            {
                curToast.Close();
            }
        }

        public ToastPosition ToastPosition
        {
            get => _toastPosition;
            set
            {
                _toastPosition = value;
                StateChanged?.Invoke();
            }
        }

        public IEnumerable<ToastState> ActiveToasts => _activeToasts;
    }
}