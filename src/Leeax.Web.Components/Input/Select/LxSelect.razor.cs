using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.AspNetCore.Components;
using System;

namespace Leeax.Web.Components.Input
{
    public partial class LxSelect<TItem> : IEnableable
    {
        public const string ClassName = "lx-select";

        private SelectIterator<TItem, IIconOption>? _iterator;
        private bool _iteratorUpdateRequired;
        private IEnumerable<TItem>? _items;
        private Color _neutralPrimary;
        private Color _backgroundColor;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _neutralPrimary = StyleContext.GetColorOrDefault(VariableNames.NeutralPrimary, default);
            _backgroundColor = StyleContext.GetColorOrDefault(VariableNames.SelectBackground, VariableNames.NeutralQuaternary);

            // Ensure that a converter got supplied
            // -> Only required when the value is not of type "IIconOption"
            if (Converter == null
                && Items != null
                && Items is not IEnumerable<IIconOption>)
            {
                throw new ApplicationException($"Cannot convert '{typeof(TItem).FullName}' to '{typeof(IIconOption).FullName}' without a converter.");
            }

            if (_iteratorUpdateRequired)
            {
                _iterator = new SelectIterator<TItem, IIconOption>(_items, Converter);
            }
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, ClassNames.Unselectable)
                .Add(ClassNames.Disabled, !IsEnabled)
                .Add(ClassNames.Extended, IsExtended)
                .AddSize(Size));

            builder.AddAttribute("role", "combobox");
        }

        protected void ToggleExtendedState() => IsExtended = !IsExtended;

        protected void OnItemClicked(TItem item)
        {
            IsExtended = false;
            ValueChanged.InvokeAsync(item);
        }

        protected string? GetItemClass(TItem item)
        {
            return ClassBuilder.Create()
                .AddMultiple("list-item", ClassNames.HoverDefault, ClassNames.ActiveDefault)
                .Add(ClassNames.Selected, item != null && item.Equals(Value))
                .Build();
        }

        private string? GetButtonClass()
        {
            return ClassBuilder.Create()
                .AddMultiple("selected-item", ClassNames.InputComponent, ClassNames.BorderRounded, ClassNames.HoverDefault, ClassNames.ActiveDefault)
                .Add(ClassNames.Border, Appearance == Appearance.Outlined)
                .AddAppearance(Appearance)
                .AddFontColor(_backgroundColor, Appearance)
                .Build();
        }

        protected bool HasValue => Value is Enum || !object.Equals(Value, default(TItem));

        public bool IsExtended { get; protected set; }

        #region Parameters

        /// <summary>
        /// Gets or sets the placeholder which gets displayed if no item is selected.
        /// </summary>
        [Parameter]
        public string? Placeholder { get; set; }

        /// <summary>
        /// Gets or sets whether the component should be enabled.
        /// </summary>
        [Parameter]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the size. The default value is <see cref="ComponentSize.Medium"/>.
        /// </summary>
        [Parameter]
        public ComponentSize Size { get; set; } = ComponentSize.Medium;

        /// <summary>
        /// Gets or sets the appearance. The default value is <see cref="Appearance.Normal"/>.
        /// </summary>
        [Parameter]
        public Appearance Appearance { get; set; } = Appearance.Normal;

        /// <inheritdoc/>
        [Parameter]
        public override IConverter? Converter
        {
            get => base.Converter;
            set
            {
                base.Converter = value;
                _iteratorUpdateRequired = true;
            }
        }

        /// <summary>
        /// Gets or sets the items to display.
        /// </summary>
        [Parameter]
        public IEnumerable<TItem>? Items
        {
            get => _items;
            set
            {
                _items = value;
                _iteratorUpdateRequired = true;
            }
        }
        #endregion
    }
}