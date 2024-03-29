﻿using System;
using System.Threading.Tasks;
using Leeax.Web.Builders;
using Leeax.Web.Components.Abstractions;
using Leeax.Web.Components.DOM;
using Leeax.Web.Components.Window;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Presentation
{
    public partial class LxDropDown : IAsyncDisposable
    {
        public const string ClassName = "lx-dropdown";

        private readonly BackwardElementReference _targetReference = new BackwardElementReference();
        private readonly BackwardElementReference _contentReference = new BackwardElementReference();

        private bool _alignContentLeftToRight = true;
        private long _callbackId = -1;

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // Register resize-handler to window
                await WindowService.EventManager
                    .AddResizeHandlerAsync(WindowResizeHandler);
            }

            // Ensure that the dropdown isn't cut off
            EnsureCorrectDropDownPosition();

            if (_callbackId > -1)
            {
                // Ensure that callback is cleaned up
                await ElementService.RemoveClickOutsideOfElementHandlerAsync(_callbackId);
                _callbackId = -1;
            }

            if (IsActive)
            {
                // Register callback if user clicks outside of dropdown
                _callbackId = await ElementService.AddClickOutsideOfElementHandlerAsync(
                    new[]
                    { 
                        _targetReference.Current,
                        _contentReference.Current 
                    }, 
                    () =>
                    {
                        _callbackId = -1;

                        IsActive = false;
                        IsActiveChanged.InvokeAsync(false); // Notify parent that dropdown closed

                        StateHasChanged();
                    });
            }
        }

        private AttributeSet? GetContentAttributeSet()
        {
            return AttributeSet.Create(builder => builder
                .AddClassAttribute(x => x
                    .Add("dropdown-content")
                    .Add("right-to-left", !_alignContentLeftToRight)
                    .AddElevation(4))
                .AddStyleAttribute(x => x
                    .AddProperty("min-width", MinWidth.ToString(), MinWidth.Value > 0)
                    .AddProperty("max-width", MaxWidth.ToString(), MaxWidth.Value > 0)));
        }
        
        private void WindowResizeHandler(EventArgs? args) => EnsureCorrectDropDownPosition();

        private void EnsureCorrectDropDownPosition()
        {
            if (!IsActive
                || !_targetReference.HasValue
                || !_contentReference.HasValue)
            {
                return;
            }

            var posParent = ElementService.GetPosition(_targetReference.Current);
            var posContent = ElementService.GetPosition(_contentReference.Current);

            if (posParent == null 
                || posContent == null)
            {
                return;
            }

            var currentAlignment = _alignContentLeftToRight;
            _alignContentLeftToRight = ((posParent.Right + posParent.Width - 30) >= posContent.Width);

            if (currentAlignment != _alignContentLeftToRight)
            {
                StateHasChanged();
            }
        }

        public async ValueTask DisposeAsync()
        {
            await WindowService.EventManager
                .RemoveResizeHandlerAsync(WindowResizeHandler);
        }

        [Inject]
        private IWindowService WindowService { get; set; } = null!;

        [Inject]
        private IElementService ElementService { get; set; } = null!;

        #region Parameters

        /// <summary>
        /// Gets or sets the min-width of the dropdown.
        /// </summary>
        [Parameter]
        public Dimension MinWidth { get; set; }

        /// <summary>
        /// Gets or sets the max-width of the dropdown.
        /// </summary>
        [Parameter]
        public Dimension MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets whether the dropdown is active/expanded.
        /// </summary>
        [Parameter]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="IsActive"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<bool> IsActiveChanged { get; set; }

        [Parameter]
        public RenderFragment? Target { get; set; }

        [Parameter]
        public RenderFragment? Content { get; set; }
        #endregion
    }
}