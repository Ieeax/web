using Leeax.Web.Internal;
using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Input
{
    [SubComponent(typeof(LxInputButton))]
    [SubComponent(typeof(LxInputIcon))]
    public partial class LxInput<TValue> : IContext, IEnableable
    {
        public const string ClassName = "lx-input";
        public const string VariableBackgroundColor = ClassName + "-background";

        private LxInputButton? _button;
        private LxInputIcon? _icon;

        private string _inputType;
        private bool _hasFocus;
        private Color _neutralPrimary;
        private Color _backgroundColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="LxInput{TValue}"/> component.
        /// The default input-type is "text".
        /// </summary>
        public LxInput()
            : this("text")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LxInput{TValue}"/> component.
        /// </summary>
        /// <param name="inputType">The type of the input element.</param>
        public LxInput(string inputType)
        {
            inputType.ThrowIfNull();
            _inputType = inputType;
        }

        protected override void OnParametersSet()
        {
            _neutralPrimary = StyleContext.GetColorOrDefault(VariableNames.NeutralPrimary, default);
            _backgroundColor = StyleContext.GetColorOrDefault(VariableBackgroundColor, VariableNames.NeutralQuaternary);
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            base.BuildAttributeSet(builder);

            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, ClassNames.BorderRounded, ClassNames.FlexRow)
                .Add(ClassNames.Border, Appearance == Appearance.Outlined)
                .Add(ClassNames.Disabled, !IsEnabled)
                .Add(ClassNames.Focused, _hasFocus)
                .AddAppearance(Appearance)
                .AddFontColor(_backgroundColor, Appearance)
                .AddSize(Size));

            builder.AddAttribute("role", "textbox");
        }

        protected void OnFocusReceived()
        {
            _hasFocus = true;
            FocusReceived.InvokeAsync(null);
        }

        protected void OnFocusLost()
        {
            _hasFocus = false;
            FocusLost.InvokeAsync(null);
        }

        protected string GetLanguageCode()
            => Culture?.TwoLetterISOLanguageName ?? "en";

        protected Task OnInputValueAsync(ChangeEventArgs args) 
            => TriggerValueCallbackAsync(args.Value?.ToString(), ValueInput);

        protected Task OnChangeValueAsync(ChangeEventArgs args) 
            => TriggerValueCallbackAsync(args.Value?.ToString(), ValueChanged);

        public void AddChild(ComponentBase component)
        {
            if (component is LxInputIcon icon)
            {
                _icon = icon;
                StateHasChanged();
            }
            else if (component is LxInputButton button)
            {
                _button = button;
                StateHasChanged();
            }
        }

        public void RemoveChild(ComponentBase component)
        {
            if (component is LxInputIcon)
            {
                _icon = null;
                StateHasChanged();
            }
            else if (component is LxInputButton)
            {
                _button = null;
                StateHasChanged();
            }
        }

        #region Parameters

        /// <summary>
        /// Gets or sets whether the component should be enabled.
        /// </summary>
        [Parameter]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        [Parameter]
        public string? Placeholder { get; set; }

        /// <summary>
        /// Gets or sets the action for the input event.
        /// </summary>
        [Parameter]
        public EventCallback<TValue> ValueInput { get; set; }

        /// <summary>
        /// Gets or sets the action for the focus-received event.
        /// </summary>
        [Parameter]
        public EventCallback FocusReceived { get; set; }

        /// <summary>
        /// Gets or sets the action for the focus-lost event.
        /// </summary>
        [Parameter]
        public EventCallback FocusLost { get; set; }

        /// <summary>
        /// Gets or sets the appearance. The default value is <see cref="Appearance.Outlined"/>.
        /// </summary>
        [Parameter]
        public Appearance Appearance { get; set; } = Appearance.Outlined;

        /// <summary>
        /// Gets or sets the size. The default value is <see cref="ComponentSize.Medium"/>.
        /// </summary>
        [Parameter]
        public ComponentSize Size { get; set; } = ComponentSize.Medium;

        /// <summary>
        /// Gets or sets the culture. Used for the "lang" attribute on the input.
        /// The default value is <see cref="CultureInfo.CurrentCulture"/>.
        /// </summary>
        [Parameter]
        public CultureInfo? Culture { get; set; } = CultureInfo.CurrentCulture;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        #endregion
    }
}