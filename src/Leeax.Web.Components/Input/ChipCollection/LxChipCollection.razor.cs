using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Input
{
    public partial class LxChipCollection : IEnableable, IDisposable
    {
        public const string ClassName = "lx-chipcollection";

        private ICollection<ChipOption>? _items;
        private IDisposable? _itemsObserver;

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            base.BuildAttributeSet(builder);

            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, ClassNames.FlexRow, ClassNames.FlexWrap)
                .Add(ClassNames.Disabled, !IsEnabled));
        }

        private Task OnItemRemoved(ChipOption item)
        {
            Items!.Remove(item);
            return ItemsChanged.InvokeAsync(Items);
        }

        private Task OnItemChanged(ChipOption item, bool value)
        {
            item.IsActive = !item.IsActive;
            return ItemsChanged.InvokeAsync(Items);
        }

        public void Dispose() 
            => _itemsObserver?.Dispose();

        #region Parameters

        /// <summary>
        /// Gets or sets whether the component should be enabled.
        /// </summary>
        [Parameter]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the appearance. The default value is <see cref="Appearance.Outlined"/>.
        /// <para />
        /// Note: <see cref="Appearance.Inline"/> is not supported.
        /// </summary>
        [Parameter]
        public Appearance Appearance { get; set; } = Appearance.Outlined;

        /// <summary>
        /// Gets or sets the size. The default value is <see cref="ComponentSize.Medium"/>.
        /// </summary>
        [Parameter]
        public ComponentSize Size { get; set; } = ComponentSize.Medium;

        /// <summary>
        /// Gets or sets the items to display.
        /// </summary>
        [Parameter]
        public ICollection<ChipOption>? Items
        { 
            get => _items; 
            set
            {
                if (_items == value) return;

                // Dispose observer for previous collection
                _itemsObserver?.Dispose();
                _items = value;

                // Create new observer for the new collection
                Observer.TryCreate(value, StateHasChanged, out _itemsObserver);
            }
        }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="Items"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<ICollection<ChipOption>> ItemsChanged { get; set; }

        /// <summary>
        /// Gets or sets whether a chip is static or can be changed by the user.
        /// </summary>
        [Parameter]
        public bool IsStatic { get; set; }

        /// <summary>
        /// Gets or sets whether any chip can be removed.
        /// </summary>
        [Parameter]
        public bool IsRemovable { get; set; }

        #endregion
    }
}