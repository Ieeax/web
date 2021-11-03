using Leeax.Web.Internal;
using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Input
{
    public partial class LxTextArea : IEnableable
    {
        public const string ClassName = "lx-textarea";

        private bool _hasFocus;
        private Color _neutralPrimary;
        private Color _backgroundColor;

        protected override void OnParametersSet()
        {
            _neutralPrimary = StyleContext.GetColorOrDefault(VariableNames.NeutralPrimary, default);
            _backgroundColor = StyleContext.GetColorOrDefault(VariableNames.TextAreaBackground, VariableNames.NeutralQuaternary);
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            base.BuildAttributeSet(builder);

            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, ClassNames.BorderRounded, ClassNames.ScrollbarThin)
                .Add(ClassNames.Border, Appearance == Appearance.Outlined)
                .Add(ClassNames.Disabled, !IsEnabled)
                .Add(ClassNames.Focused, _hasFocus)
                .AddAppearance(Appearance)
                .AddTextWrap(TextWrap)
                .AddFontColor(_backgroundColor, Appearance));

            builder.AddStyleAttribute(x => x
                .AddProperty("resize", ResizeAxis switch
                {
                    ResizeAxis.Horizontal => "horizontal",
                    ResizeAxis.Vertical => "vertical",
                    ResizeAxis.Both => "both",
                    _ => "none"
                }));
            
            builder.AddAttribute("readonly", string.Empty, IsReadOnly);
            builder.AddAttribute("wrap", "hard");
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
            => ValueInput.InvokeAsync(args.Value?.ToString());

        protected Task OnChangeValueAsync(ChangeEventArgs args)
            => ValueChanged.InvokeAsync(args.Value?.ToString());

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
        /// Gets or sets whether the text is read-only.
        /// </summary>
        [Parameter]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the action for the input event.
        /// </summary>
        [Parameter]
        public EventCallback<string> ValueInput { get; set; }

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
        /// Gets or sets the appearance.
        /// The default value is <see cref="Appearance.Outlined"/>.
        /// </summary>
        [Parameter]
        public Appearance Appearance { get; set; } = Appearance.Outlined;

        /// <summary>
        /// Gets or sets the axis in which resizing is allowed.
        /// The default value is <see cref="ResizeAxis.None"/>.
        /// </summary>
        [Parameter]
        public ResizeAxis ResizeAxis { get; set; } = ResizeAxis.None;

        /// <summary>
        /// Gets or sets how text is wrapped.
        /// The default value is <see cref="TextWrap.Anywhere"/>.
        /// </summary>
        [Parameter]
        public TextWrap TextWrap { get; set; } = TextWrap.Anywhere;
        
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