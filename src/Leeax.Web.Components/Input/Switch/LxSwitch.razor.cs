using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Input
{
    public partial class LxSwitch<TItem> : IEnableable, IAsyncDisposable
    {
        public const string ClassName = "lx-switch";
        public const string VariableBackgroundColor = ClassName + "-background";
        public const string VariableSelectedBackgroundColor = ClassName + "-selected-background";

        private IJSInProcessRuntime? _jsInProcessRuntime;
        private IJSInProcessObjectReference? _jsReference;
        private ElementReference _switchBarReference;
        private ElementReference _switchIndicatorReference;

        private SelectIterator<TItem, IOption>? _iterator;
        private bool _iteratorUpdateRequired;
        private IEnumerable<TItem>? _items;
        private Color _backgroundColor;

        protected override void OnParametersSet()
        {
            _backgroundColor = StyleContext.GetColorOrDefault(VariableBackgroundColor, VariableNames.NeutralQuaternary);

            // Select first item if no value is selected
            if (Value == null
                && Items != null)
            {
                Value = Items.FirstOrDefault();
            }

            // Ensure that a converter got supplied
            // -> Only required when the value is not of type "IOption"
            if (Converter == null
                && Items != null
                && Items is not IEnumerable<IOption>)
            {
                throw new ApplicationException($"Cannot convert '{typeof(TItem).FullName}' to '{typeof(IOption).FullName}' without a converter.");
            }

            if (_iteratorUpdateRequired)
            {
                _iterator = new SelectIterator<TItem, IOption>(_items, Converter);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _jsInProcessRuntime = (IJSInProcessRuntime)JSRuntime;
                _jsReference = await _jsInProcessRuntime
                    .InvokeAsync<IJSInProcessObjectReference>("import", "./_content/Leeax.Web.Components/Switch.js");
            }

            // Ensure that reference was resolved (important!)
            // Lifecycle method could be called twice very fast:
            // If that's the case the second call (is not "firstRender") would 
            // try to call a function on the js-object which is not yet resolved
            if (_jsReference != null)
            {
                var indexOfValue = FindIndexOfValue();
                if (indexOfValue > -1)
                {
                    MoveIndicator(indexOfValue);
                }
            }
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, ClassNames.Unselectable)
                .Add(ClassNames.Disabled, !IsEnabled)
                .AddAppearance(Appearance)
                .AddFontColor(_backgroundColor)
                .AddTextTransform(TextTransform)
                .AddSize(Size));
        }

        private int FindIndexOfValue()
        {
            if (Value == null
                || Items == null)
            {
                return -1;
            }

            var indexOfValue = -1;
            foreach (var item in Items)
            {
                indexOfValue++;

                if (object.Equals(Value, item))
                {
                    break;
                }
            }

            return indexOfValue;
        }

        // This method can only be called when the js-references are resolved
        private bool MoveIndicator(int index)
        {
            return _jsReference!.Invoke<bool>(
                "move",
                _switchBarReference,
                _switchIndicatorReference,
                index);
        }

        private async Task OnItemClicked(TItem item)
        {
            Value = item;
            await ValueChanged.InvokeAsync(item);
        }

        public ValueTask DisposeAsync()
        {
            return _jsReference == null
                ? ValueTask.CompletedTask
                : _jsReference.DisposeAsync();
        }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        #region Parameters

        /// <summary>
        /// Gets or sets whether the component should be enabled.
        /// </summary>
        [Parameter]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the text-transformation.
        /// Is equal to the "text-transform" CSS property.
        /// </summary>
        [Parameter]
        public TextTransform TextTransform { get; set; }

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
        /// Gets or sets the selected value.
        /// </summary>
        [Parameter]
        public override TItem? Value
        { 
            get => base.Value;
            set
            {
                // Ignore null values
                if (value != null)
                {
                    base.Value = value;
                }
            }
        }

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