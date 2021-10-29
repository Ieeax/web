using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Leeax.Web.Components.Presentation
{
    [SubComponent(typeof(LxTableViewColumn))]
    [SubComponent(typeof(LxTableViewIconColumn))]
    [SubComponent(typeof(LxTableViewLinkColumn))]
    public partial class LxTableView : IDisposable
    {
        public const string ClassName = "lx-tableview";
        public const string VariableAccentColor = ClassName + "-accent-color";

        private readonly TableViewContext _context = new TableViewContext();
        private IEnumerable<object?>? _items;
        private IEnumerable<object?>? _itemsSorted;

        private Color _directionIconColor;

        protected override void OnInitialized()
        {
            _context.ColumnChanged += OnColumnChanged;
        }

        protected override void OnParametersSet()
        {
            _directionIconColor = StyleContext.GetColorOrDefault(VariableAccentColor, VariableNames.ThemePrimary);
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName, ClassNames.BorderRounded);
            builder.AddAttribute("role", "grid");
        }

        private void OnColumnChanged(object? sender, EventArgs args)
            => StateHasChanged();

        private string? GetHeaderClass(TableViewColumnBase column)
        {
            return ClassBuilder.Create("tv-cell-inner", "tv-cell-action", ClassNames.BorderRounded)
                .AddMultiple(new[] { "sortable", ClassNames.HoverDefault, ClassNames.ActiveDefault }, column.Header != null && column.SortingEnabled)
                .Add("sorted-" + (SortDirection == SortDirection.Descending ? "desc" : "asc"), column == SortedColumn)
                .Build();
        }

        private string? GetBodyCellDivClass()
        {
            return ClassBuilder.Create("tv-cell-inner")
                .Build();
        }

        private void OnHeaderClicked(TableViewColumnBase column)
        {
            if (!column.SortingEnabled)
            {
                return;
            }

            if (SortedColumn == column)
            {
                // If column is already sorted, change the direction
                SortDirection = SortDirection switch
                {
                    SortDirection.Ascending => SortDirection.Descending,
                    SortDirection.Descending => SortDirection.None,
                    _ => SortDirection.Ascending
                };

                if (SortDirection == SortDirection.None)
                {
                    SortedColumn = null;
                }
            }
            else
            {
                // Set the column to be sorted by ascending order
                SortedColumn = column;
                SortDirection = SortDirection.Ascending;
            }

            EnsureItemsAreSorted();
        }

        private void EnsureItemsAreSorted()
        {
            if (SortDirection == SortDirection.None)
            {
                if (_itemsSorted != null)
                {
                    _itemsSorted = null;
                }

                return;
            }

            if (SortedColumn == null
                || Items == null)
            {
                return;
            }

            var comparer = SortedColumn.Comparer;
            if (comparer == null)
            {
                return;
            }

            var list = Items.ToList();

            // Sort collection by given comparer
            list.Sort((x, y) =>
            {
                return (SortDirection == SortDirection.Descending)
                    ? comparer.Compare(x, y) * -1
                    : comparer.Compare(x, y);
            });

            _itemsSorted = list;
        }

        private IEnumerable<object?> GetSortedItems()
        {
            if (Items == null)
            {
                return Enumerable.Empty<object?>();
            }

            return _itemsSorted ?? Items;
        }

        private void BuildRenderTreeForSpacer(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "td");
            builder.AddAttribute(1, "class", "tv-cell-spacer");
            builder.AddAttribute(2, "role", "presentation");
            builder.OpenElement(3, "div");
            builder.AddAttribute(4, "class", GetBodyCellDivClass());
            builder.CloseElement();
            builder.CloseElement();
        }

        private RenderFragment BuildRenderTreeForRow(object? item)
        {
            return (builder) =>
            {
                builder.AddContent(0, BuildRenderTreeForSpacer);

                foreach (var curItem in _context.ColumnDefinitions)
                {
                    builder.OpenElement(1, "td");
                    builder.AddAttribute(2, "class", ("tv-cell " + curItem.ClassNameCell).TrimEnd());
                    builder.OpenElement(3, "div");
                    builder.AddAttribute(4, "style", curItem.MaxWidth.Value <= 0 ? null : "max-width:" + curItem.MaxWidth.ToString());
                    builder.AddAttribute(5, "class", GetBodyCellDivClass());
                    builder.AddContent(6, builder => curItem.BuildRenderTree(builder, item));
                    builder.CloseElement();
                    builder.CloseElement();
                }

                builder.AddContent(7, BuildRenderTreeForSpacer);
            };
        }

        public void Dispose()
        {
            _context.ColumnChanged -= OnColumnChanged;
        }

        /// <summary>
        /// Gets the column that was used for sorting.
        /// </summary>
        public TableViewColumnBase? SortedColumn { get; private set; }

        /// <summary>
        /// Gets the sort-direction for the selected column.
        /// </summary>
        public SortDirection SortDirection { get; private set; }

        /// <summary>
        /// Gets or sets the items to display.
        /// </summary>
        [Parameter]
        public IEnumerable<object?>? Items
        {
            get => _items;
            set
            {
                _items = value;
                EnsureItemsAreSorted();
            }
        }

        /// <summary>
        /// Gets or sets the callback to execute whenever a row was clicked.
        /// </summary>
        [Parameter]
        public EventCallback<object?> RowClicked { get; set; }

        /// <summary>
        /// Gets or sets the RenderFragment within columns can be defined.
        /// </summary>
        [Parameter]
        public RenderFragment? ColumnDefinitions { get; set; }
    }
}