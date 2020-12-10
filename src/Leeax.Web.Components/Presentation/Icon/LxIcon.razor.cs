using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using Microsoft.AspNetCore.Components;
using System.Drawing;

namespace Leeax.Web.Components.Presentation
{
    public partial class LxIcon
    {
        public const string ClassName = "lx-icon";
        public const string VariableColor = ClassName + "-color";
        private const string AltDefaultValue = "icon";

        private string? _source;
        private string? _staticSource;
        private Color _fillColor;

        protected override void OnParametersSet()
        {
            _fillColor = StyleContext.GetColorOrDefault(VariableColor, VariableNames.NeutralBlack);
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName);

            builder.AddStyleAttribute(x => x
                .AddProperty("height", Size.ToString())
                .AddProperty("min-height", Size.ToString())
                .AddProperty("width", Size.ToString())
                .AddProperty("min-width", Size.ToString()));
        }

        [Inject]
        private IIconProvider IconProvider { get; set; }

        #region Parameters

        /// <summary>
        /// Gets or sets the height and width of the icon. The default value is 1.5rem.
        /// </summary>
        [Parameter]
        public Length Size { get; set; } = new Length(1.5, Unit.REM);

        /// <summary>
        /// Gets or sets the source (-url) from which the icon is loaded.
        /// To use icons from the current <see cref="IIconProvider"/>, add <c>static://</c> before the icon name.
        /// </summary>
        [Parameter]
        public string? Source
        {
            get => _source;
            set
            {
                if (_source == value)
                {
                    return;
                }

                _source = value;                    
                _staticSource = null;
                
                // Check whether the string starts with "static://"
                // -> If so, we need to load the icon from the IconResolver
                if (_source != null
                    && _source.StartsWith("static://"))
                {
                    _staticSource = _source.Length == 9
                        ? string.Empty 
                        : _source[9..];
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of static icons.
        /// Specify a color for each theme to provide the best user experience.
        /// </summary>
        [Parameter]
        public Color Fill { get; set; }

        /// <summary>
        /// Gets or sets the alternative text.
        /// Gets displayed if the icon couldn't be loaded.
        /// </summary>
        [Parameter]
        public string? Alt { get; set; }
        #endregion
    }
}