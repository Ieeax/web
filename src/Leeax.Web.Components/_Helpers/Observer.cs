using Leeax.Web.Internal;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Leeax.Web.Components
{
    public class Observer : IDisposable
    {
        private readonly Action _stateChangedHandler;
        private readonly IEnumerable _enumerable;
        private bool _disposed;

        private Observer(IEnumerable enumerable, Action stateChangedHandler)
        {
            enumerable.ThrowIfNull();
            stateChangedHandler.ThrowIfNull();

            _enumerable = enumerable;
            _stateChangedHandler = stateChangedHandler;

            if (enumerable is INotifyCollectionChanged a)
            {
                a.CollectionChanged += CollectionChanged;
                AttachPropertyChangedHandlers();
            }
        }

        public static bool TryCreate(IEnumerable? enumerable, Action stateChangedHandler, out IDisposable? observer)
        {
            stateChangedHandler.ThrowIfNull();

            if (enumerable is INotifyCollectionChanged)
            {
                observer = new Observer(enumerable, stateChangedHandler);
                return true;
            }

            observer = null;
            return false;
        }

        public static IDisposable Create(IEnumerable enumerable, Action stateChangedHandler)
            => new Observer(enumerable, stateChangedHandler);

        private void AttachPropertyChangedHandlers()
        {
            if (_disposed) return;

            foreach (var curItem in _enumerable)
            {
                if (curItem is INotifyPropertyChanged a)
                {
                    a.PropertyChanged += PropertyChanged;
                }
            }
        }

        private void DetachPropertyChangedHandlers()
        {
            if (_disposed) return;

            foreach (var curItem in _enumerable)
            {
                if (curItem is INotifyPropertyChanged a)
                {
                    a.PropertyChanged -= PropertyChanged;
                }
            }
        }

        private void PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_disposed) return;

            _stateChangedHandler.Invoke();
        }

        private void CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (_disposed) return;

            // Ensure that all handlers get detached from old items and attached to new ones
            if (e.Action != NotifyCollectionChangedAction.Move)
            {
                if (e.OldItems != null)
                {
                    foreach (var curItem in e.OldItems)
                    {
                        if (curItem is INotifyPropertyChanged a)
                        {
                            a.PropertyChanged -= PropertyChanged;
                        }
                    }
                }

                if (e.NewItems != null)
                {
                    foreach (var curItem in e.NewItems)
                    {
                        if (curItem is INotifyPropertyChanged a)
                        {
                            a.PropertyChanged += PropertyChanged;
                        }
                    }
                }
            }

            _stateChangedHandler.Invoke();
        }

        public void Dispose()
        {
            if (!_disposed
                && _enumerable is INotifyCollectionChanged a)
            {
                a.CollectionChanged -= CollectionChanged;
                DetachPropertyChangedHandlers();

                _disposed = true;
            }
        }
    }
}