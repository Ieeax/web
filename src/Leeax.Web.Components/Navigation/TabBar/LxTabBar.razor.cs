using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Navigation
{
    public partial class LxTabBar<TItem>
    {
        public const string ClassName = "lx-tabbar";

        private SelectIterator<TItem, IIconOption>? _iterator;
        private bool _iteratorUpdateRequired;
        private IEnumerable<TItem>? _items;
        private Color _backgroundColor;
        private Color _activeBackgroundColor;

        protected override void OnParametersSet()
        {
            _backgroundColor = StyleContext.GetColorOrDefault(VariableNames.TabBarColor, VariableNames.NeutralSecondary);
            _activeBackgroundColor = StyleContext.GetColorOrDefault(VariableNames.TabBarActiveColor, VariableNames.ThemePrimary);

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
                .AddMultiple(ClassName, ClassNames.Unselectable, ClassNames.FlexRow)
                .AddTextTransform(TextTransform)
                .AddSize(Size));
        }

        private string? GetTabItemClass(TItem item)
        {
            return ClassBuilder.Create("tab-header")
                .Add(ClassNames.FontWeightSemibold)
                .Add(ClassNames.Active, object.Equals(item, Value))
                .Build();
        }

        private string? GetTabItemLineClass(TItem item)
        {
            return ClassBuilder.Create("tab-line")
                .Add(ClassNames.ElevationLevel2, Appearance == Appearance.Raised && object.Equals(item, Value))
                .Build();
        }

        private Color GetIconColor(TItem item)
        {
            return object.Equals(item, Value)
                ? _activeBackgroundColor
                : _backgroundColor;
        }

        private void OnTabItemClicked(TItem item)
        {
            if (!object.Equals(Value, item))
            {
                ValueChanged.InvokeAsync(item);
            }
        }

        #region Parameters

        /// <summary>
        /// Gets or sets the appearance. The default value is <see cref="Appearance.Normal"/>.
        /// <para />
        /// Note: <see cref="Appearance.Inline"/> and <see cref="Appearance.Outlined"/> are not supported.
        /// </summary>
        [Parameter]
        public Appearance Appearance { get; set; } = Appearance.Normal;

        /// <summary>
        /// Gets or sets the size. The default value is <see cref="ComponentSize.Medium"/>.
        /// </summary>
        [Parameter]
        public ComponentSize Size { get; set; } = ComponentSize.Medium;

        /// <summary>
        /// Gets or sets the text-transformation.
        /// Is equal to the "text-transform" CSS property.
        /// </summary>
        [Parameter]
        public TextTransform TextTransform { get; set; }

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
        /// Gets or sets the tabs to display.
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