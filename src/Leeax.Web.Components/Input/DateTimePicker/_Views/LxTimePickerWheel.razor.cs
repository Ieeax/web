using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Input
{
    public partial class LxTimePickerWheel : IAsyncDisposable
    {
        public const string ClassName = "lx-timepickerwheel";
        private const double ItemHeight = 2.5;
        private const double ItemMargin = 0.2;

        private IJSInProcessRuntime? _jsInProcessRuntime;
        private IJSInProcessObjectReference? _jsReference;
        private IJSInProcessObjectReference? _jsReferenceInstance;
        private ElementReference _rootElement;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _jsInProcessRuntime = (IJSInProcessRuntime)JSRuntime;
                _jsReference = await _jsInProcessRuntime
                    .InvokeAsync<IJSInProcessObjectReference>("import", "./_content/Leeax.Web.Components/TimePickerWheel.js");
                _jsReferenceInstance = _jsReference
                    .Invoke<IJSInProcessObjectReference>("create", DotNetObjectReference.Create(this));

                // Register javascript event-listener
                _jsReferenceInstance.InvokeVoid("addOrUpdateWheelListener", _rootElement);
            }
        }

        private string GetStyle()
            => $"height:{((ItemHeight + ItemMargin) * 5)}em;";

        private string? GetItemClass(int index)
        {
            return ClassBuilder.Create()
                .AddMultiple(ClassNames.Unselectable, ClassNames.OverflowHidden, ClassNames.BorderRounded)
                .Add(ClassNames.Selected + " " + ClassNames.ThemePrimaryForegroundColor, Value == index)
                .Add(ClassNames.HoverDefault + " " + ClassNames.ActiveDefault, Value != index)
                .Add("prev-item", (Value - 1) == index)
                .Add("next-item", (Value + 1) == index)
                .Build();
        }

        private string? GetItemWrapperStyle()
        {
            return CSSBuilder.Create()
                .AddProperty("transform", $"translateY({(-(Value * (ItemHeight + ItemMargin)) + ((ItemHeight + ItemMargin) * 4) / 2)}em)")
                .Build();
        }

        private void OnItemClicked(int index)
        {
            if (Value == index) return;

            ValueChanged.InvokeAsync(index);
        }

        public async ValueTask DisposeAsync()
        {
            if (_jsReference != null)
            {
                await _jsReference!.DisposeAsync();
            }

            if (_jsReferenceInstance != null)
            {
                _jsReferenceInstance.InvokeVoid("removeWheelListener");
                await _jsReferenceInstance.DisposeAsync();
            }
        }

        [JSInvokable]
        public void HandleWheelEventCallback(int deltaY)
        {
            if (deltaY > 0)
            {
                var value = Value + 1;
                if (value > MaxValue)
                {
                    value = MinValue;
                }

                ValueChanged.InvokeAsync(value);
            }
            else if (deltaY < 0)
            {
                var value = Value - 1;
                if (value < MinValue)
                {
                    value = MaxValue;
                }

                ValueChanged.InvokeAsync(value);
            }
        }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        /// <summary>
        /// Gets or sets the minimal value.
        /// </summary>
        [Parameter]
        public int MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        [Parameter]
        public int MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Parameter]
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="Value"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }
    }
}