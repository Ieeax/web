using Leeax.Web.Builders;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Presentation
{
    public partial class ListView<TItem>
    {
        public const string ClassName = "lx-listview";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .Add(ClassName)
                .Add(ClassNames.FlexRow, ClassNames.FlexColumn, Orientation == Orientation.Horizontal));
        }

        private AttributeSet? GetItemAttributeSet(TItem? item)
        {
            if (ItemAttributeSetFactory != null)
            {
                return ItemAttributeSetFactory.Invoke(item);
            }

            return ItemAttributeSet;
        }

        /// <summary>
        /// Gets or sets the orientation. The default value is <see cref="Orientation.Vertical"/>.
        /// </summary>
        [Parameter]
        public Orientation Orientation { get; set; } = Orientation.Vertical;

        /// <summary>
        /// Gets or sets the items to display.
        /// </summary>
        [Parameter]
        public IEnumerable<TItem?>? Items { get; set; }

        /// <summary>
        /// Gets or sets a factory for creating an <see cref="AttributeSet"/> for each &lt;li&gt; item.
        /// </summary>
        [Parameter]
        public Func<TItem?, AttributeSet>? ItemAttributeSetFactory { get; set; }

        /// <summary>
        /// Gets or sets an <see cref="AttributeSet"/> which gets applied to each &lt;li&gt; item.
        /// </summary>
        [Parameter]
        public AttributeSet? ItemAttributeSet { get; set; }

        /// <summary>
        /// Gets or sets the callback to execute whenever an item was clicked.
        /// </summary>
        [Parameter]
        public EventCallback<TItem?> ItemClicked { get; set; }

        [Parameter]
        public RenderFragment<TItem?>? ItemTemplate { get; set; }
    }
}