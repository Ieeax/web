using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Drawing;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Input
{
    [SubComponent(typeof(LxButtonIcon))]
    public partial class LxButton : IContext, IEnableable
    {
        public const string ClassName = "lx-button";
        public const string VariableBackgroundColor = ClassName + "-background";
        public const string VariableIconColor = ClassName + "-icon";

        private LxButtonIcon? _icon;

        private bool _isActive;
        private Color _backgroundColor;
        private Color _iconColor;
        private Color _neutralPrimary;

        protected override void OnParametersSet()
        {
            _neutralPrimary = StyleContext.GetColorOrDefault(VariableNames.NeutralPrimary, default);
            _backgroundColor = StyleContext.GetColorOrDefault(VariableBackgroundColor, VariableNames.NeutralQuaternary);
            
            _iconColor = StyleContext.TryGetColor(VariableIconColor, out var iconColor) 
                ? iconColor 
                : ComponentHelper.GetForegroundColor(Appearance, _backgroundColor, _neutralPrimary);
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, ClassNames.Unselectable, ClassNames.HoverDefault, ClassNames.ActiveDefault)
                .Add(ClassNames.Border, Appearance == Appearance.Outlined)
                .Add(ClassNames.FlexReverse, HasText && HasIcon && _icon!.Trailing)
                .Add(ClassNames.Disabled, !IsEnabled)
                .Add(ClassNames.Active, _isActive)
                .Add("lx-button-icon", HasIcon && !HasText)
                .Add("lx-button-round", ClassNames.BorderRounded, IsRounded)
                .AddTextTransform(TextTransform)
                .AddFontColor(_backgroundColor, Appearance)
                .AddAppearance(Appearance)
                .AddSize(Size));

            builder.AddAttribute("role", "button");
            builder.AddAttribute("data-lx-interaction", "3");
        }

        private string? GetIconClass()
        {
            return ClassBuilder.Create()
                .Add("lx-trailing", _icon != null && _icon.Trailing)
                .Add("m-auto", "my-auto", HasIcon && !HasText)
                .Build();
        }

        protected async Task RaiseClickedEvent(MouseEventArgs args)
        {
            if (_isActive)
            {
                return;
            }

            try
            {
                if (Clicked.HasDelegate)
                {
                    _isActive = true;

                    // Re-render component with "active" class
                    StateHasChanged();

                    // Invoke action
                    await Clicked.InvokeAsync(args);
                }
                else if (Link != null)
                {
                    NavManager!.NavigateTo(Link);
                }
            }
            finally
            {
                _isActive = false;
            }
        }

        public void AddChild(ComponentBase component)
        {
            if (component is LxButtonIcon icon)
            {
                _icon = icon;
                StateHasChanged();
            }
        }

        public void RemoveChild(ComponentBase component)
        {
            if (component is LxButtonIcon)
            {
                _icon = null;
                StateHasChanged();
            }
        }

        protected bool HasText => _icon == null || !string.IsNullOrEmpty(Text);

        protected bool HasIcon => _icon != null;

        [Inject]
        private NavigationManager NavManager { get; set; } = null!;

        #region Parameters

        /// <summary>
        /// Gets or sets whether the component should be enabled.
        /// </summary>
        [Parameter]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets whether the button has rounded corners.
        /// </summary>
        [Parameter]
        public bool IsRounded { get; set; }

        /// <summary>
        /// Gets or sets the text-transformation.
        /// Is equal to the "text-transform" CSS property.
        /// </summary>
        [Parameter]
        public TextTransform TextTransform { get; set; }

        /// <summary>
        /// Gets or sets the appearance. The default value is <see cref="Appearance.Normal"/>.
        /// </summary>
        [Parameter]
        public Appearance Appearance { get; set; } = Appearance.Normal;

        /// <summary>
        /// Gets or sets the size. The default value is <see cref="ComponentSize.Medium"/>.
        /// </summary>
        [Parameter]
        public ComponentSize Size { get; set; } = ComponentSize.Medium;

        /// <summary>
        /// Gets or sets a "href".
        /// </summary>
        [Parameter]
        public string? Link { get; set; }

        /// <summary>
        /// Gets or sets the callback to execute whenever the button was clicked.
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> Clicked { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        #endregion
    }
}