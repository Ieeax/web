using Leeax.Web.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Leeax.Web.Components.Presentation
{
    public class TableViewContext
    {
        private readonly HashSet<TableViewColumnBase> _columnDefinitions;

        public event EventHandler? ColumnChanged;

        public TableViewContext()
        {
            _columnDefinitions = new HashSet<TableViewColumnBase>();
        }

        public void AddColumn(TableViewColumnBase value)
        {
            value.ThrowIfNull();

            if (!_columnDefinitions.Add(value))
            {
                throw new ApplicationException($"Passed column '{value.GetType().FullName}' was already defined. Ensure each column gets defined only once.");
            }

            value.PropertyChanged += OnColumnPropertyChanged;
            ColumnChanged?.Invoke(value, EventArgs.Empty);
        }

        public void RemoveColumn(TableViewColumnBase value)
        {
            value.ThrowIfNull();

            if (_columnDefinitions.Remove(value))
            {
                value.PropertyChanged -= OnColumnPropertyChanged;
                ColumnChanged?.Invoke(value, EventArgs.Empty);
            }
        }

        private void OnColumnPropertyChanged(object? sender, PropertyChangedEventArgs args)
        {
            ColumnChanged?.Invoke(sender, EventArgs.Empty);
        }

        public IReadOnlyCollection<TableViewColumnBase> ColumnDefinitions => _columnDefinitions;
    }
}