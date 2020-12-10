using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Input
{
    public partial class LxTagBase : IEnableable
    {
        public const string ClassName = "lx-tagbase";
        public const string VariableBackgroundColor = ClassName + "-background";
        public const string VariableActiveBackgroundColor = ClassName + "-active-background";

        private readonly string _className;
        private readonly bool _rounded;
        private Color _neutralPrimary;
        private Color _backgroundColor;
        private Color _activeBackgroundColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="LxTagBase"/> component.
        /// </summary>
        public LxTagBase(string className, bool rounded)
        {
            _className = className;
            _rounded = rounded;
        }

        protected override void OnParametersSet()
        {
            _neutralPrimary = StyleContext.GetColorOrDefault(VariableNames.NeutralPrimary, default);
            _backgroundColor = StyleContext.GetColorOrDefault(VariableBackgroundColor, VariableNames.NeutralQuaternary);
            _activeBackgroundColor = StyleContext.GetColorOrDefault(VariableActiveBackgroundColor, VariableNames.ThemePrimary);
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, _className, ClassNames.Unselectable)
                .Add(ClassNames.HoverDefault + " " + ClassNames.ActiveDefault, IsInteractable())
                .Add(ClassNames.BorderRounded, !_rounded)
                .Add(ClassNames.Border, !IsActive && Appearance == Appearance.Outlined)
                .Add(ClassNames.Disabled, !IsEnabled)
                .Add(ClassNames.Active, IsActive)
                .AddFontColor(IsActive ? _activeBackgroundColor : _backgroundColor, IsActive || Appearance != Appearance.Outlined)
                .AddAppearance(Appearance)
                .AddSize(Size));

            builder.AddAttribute("data-lx-interaction", "3");
        }

        protected override Task OnParametersSetAsync()
        {
            if (IsRemovable
                && !IsActive)
            {
                IsActive = true;
                return IsActiveChanged.InvokeAsync(IsActive);
            }

            return Task.CompletedTask;
        }

        protected virtual bool IsInteractable() => !IsStatic && !IsRemovable;

        protected virtual Task OnToggle()
        {
            if (IsRemovable
                || IsStatic)
            {
                return Task.CompletedTask;
            }

            IsActive = !IsActive;
            return IsActiveChanged.InvokeAsync(IsActive);
        }

        protected virtual Task OnRemove()
        {
            return Removed.InvokeAsync();
        }

        private Color GetIconColor()
        {
            return (Appearance == Appearance.Outlined || _backgroundColor == Color.Transparent) && !IsActive
                ? _neutralPrimary
                : ColorHelper.IsDarkColor(IsActive ? _activeBackgroundColor : _backgroundColor)
                    ? Color.White
                    : Color.Black;
        }

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
        /// Gets or sets whether the tag is static or can be changed by the user.
        /// </summary>
        [Parameter]
        public bool IsStatic { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the tag should be active.
        /// </summary>
        [Parameter]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="IsActive"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<bool> IsActiveChanged { get; set; }

        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        [Parameter]
        public string? Label { get; set; }

        /// <summary>
        /// Gets or sets the icon source.
        /// </summary>
        [Parameter]
        public virtual string? Icon { get; set; }

        /// <summary>
        /// Gets or sets whether the tag should display a button which triggers the <see cref="Removed"/> callback.
        /// </summary>
        [Parameter]
        public bool IsRemovable { get; set; }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever the "remove" button gets clicked.
        /// </summary>
        [Parameter]
        public EventCallback Removed { get; set; }
        #endregion
    }
}