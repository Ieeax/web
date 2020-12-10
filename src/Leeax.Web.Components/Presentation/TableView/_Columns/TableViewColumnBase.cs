using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Components.Presentation
{
    public abstract class TableViewColumnBase : ComponentBase, INotifyPropertyChanged, IDisposable
    {
        private string? _header;
        private Length _maxWidth;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected override void OnInitialized()
        {
            if (Context == null)
            {
                throw new ApplicationException($"Required cascading value of type \"{nameof(TableViewContext)}\" wasn't supplied.");
            }

            // Register column to the context
            Context.AddColumn(this);
        }

        public abstract void BuildRenderTree(RenderTreeBuilder builder, object? value);

        protected void RaisePropertyChanged<TValue>(ref TValue? member, TValue? value, [CallerMemberName] string? propertyName = null)
        {
            if (!object.Equals(member, value))
            {
                member = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose()
        {
            // Remove column from the context
            Context?.RemoveColumn(this);
        }

        /// <summary>
        /// Gets the class for the &lt;td&gt; element.
        /// </summary>
        public string? ClassNameCell { get; protected set; }

        /// <summary>
        /// Gets whether the column can be sorted by.
        /// </summary>
        public bool SortingEnabled { get; protected set; }

        /// <summary>
        /// Gets the comparer for sorting.
        /// </summary>
        public IComparer<object?>? Comparer { get; protected set; }

        /// <summary>
        /// Gets the context of the parent component.
        /// </summary>
        [CascadingParameter]
        public TableViewContext Context { get; set; }

        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        [Parameter]
        public string? Header
        {
            get => _header;
            set => RaisePropertyChanged(ref _header, value);
        }

        /// <summary>
        /// Gets or sets the max-width.
        /// </summary>
        [Parameter]
        public Length MaxWidth
        {
            get => _maxWidth;
            set => RaisePropertyChanged(ref _maxWidth, value);
        }
    }
}