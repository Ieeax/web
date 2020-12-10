using System;
using Leeax.Web.Builders;
using Leeax.Web.Components.Abstractions;
using Leeax.Web.Components.DOM;
using Leeax.Web.Components.Window;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Presentation
{
    public partial class LxDropDown : IDisposable
    {
        public const string ClassName = "lx-dropdown";
        public const string VariableBackgroundColor = ClassName + "-background";

        private readonly BackwardElementReference _targetReference = new BackwardElementReference();
        private readonly BackwardElementReference _contentReference = new BackwardElementReference();

        private bool _alignContentLeftToRight = true;
        private long _callbackId = -1;

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                // Register resize-handler to window
                WindowService.EventManager
                    .AddResizeHandler(WindowResizeHandler);
            }

            // Ensure that the dropdown isn't cut off
            EnsureCorrectDropDownPosition();

            if (_callbackId > -1)
            {
                // Ensure that callback is cleaned up
                ElementService.RemoveClickOutsideOfElementHandler(_callbackId);
                _callbackId = -1;
            }

            if (IsActive)
            {
                // Register callback if user clicks outside of dropdown
                _callbackId = ElementService.AddClickOutsideOfElementHandler(
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
            _alignContentLeftToRight = ((posParent.Right + posParent.Width) >= posContent.Width);

            if (currentAlignment != _alignContentLeftToRight)
            {
                StateHasChanged();
            }
        }

        public void Dispose()
        {
            WindowService.EventManager
                .RemoveResizeHandler(WindowResizeHandler);
        }

        [Inject]
        private IWindowService WindowService { get; set; }

        [Inject]
        private IElementService ElementService { get; set; }

        #region Parameters

        /// <summary>
        /// Gets or sets the min-width of the dropdown.
        /// </summary>
        [Parameter]
        public Length MinWidth { get; set; }

        /// <summary>
        /// Gets or sets the max-width of the dropdown.
        /// </summary>
        [Parameter]
        public Length MaxWidth { get; set; }

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