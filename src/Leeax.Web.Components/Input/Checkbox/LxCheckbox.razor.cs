using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using Microsoft.AspNetCore.Components;
using System.Drawing;

namespace Leeax.Web.Components.Input
{
    public partial class LxCheckbox : IEnableable
    {
        public const string ClassName = "lx-checkbox";
        public const string VariableBackgroundColor = ClassName + "-background";
        public const string VariableActiveBackgroundColor = ClassName + "-active-background";

        private Color _backgroundColor;
        private Color _activeBackgroundColor;

        protected override void OnParametersSet()
        {
            _backgroundColor = StyleContext.GetColorOrDefault(VariableBackgroundColor, VariableNames.NeutralQuaternary);
            _activeBackgroundColor = StyleContext.GetColorOrDefault(VariableActiveBackgroundColor, VariableNames.ThemePrimary);
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            base.BuildAttributeSet(builder);

            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, ClassNames.BorderRounded, ClassNames.HoverDefault, ClassNames.ActiveDefault, ClassNames.Unselectable)
                .Add(ClassNames.Border, !Value && Appearance == Appearance.Outlined)
                .Add(ClassNames.Disabled, !IsEnabled)
                .Add(ClassNames.Active, Value)
                .AddAppearance(Appearance)
                .AddSize(Size));

            builder.AddAttribute("role", "checkbox");
            builder.AddAttribute("data-lx-interaction", "3");
        }

        protected void OnValueChanged(ChangeEventArgs args) => ValueChanged.InvokeAsync(args.Value is bool value && value);

        #region Parameters

        /// <summary>
        /// Gets or sets whether the component should be enabled.
        /// </summary>
        [Parameter]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the appearance. The default value is <see cref="Appearance.Normal"/>.
        /// <para />
        /// Note: <see cref="Appearance.Inline"/> is not supported.
        /// </summary>
        [Parameter]
        public Appearance Appearance { get; set; } = Appearance.Normal;

        /// <summary>
        /// Gets or sets the size. The default value is <see cref="ComponentSize.Medium"/>.
        /// </summary>
        [Parameter]
        public ComponentSize Size { get; set; } = ComponentSize.Medium;
        #endregion
    }
}