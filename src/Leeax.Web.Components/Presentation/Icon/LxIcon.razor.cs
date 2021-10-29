using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using Microsoft.AspNetCore.Components;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Presentation
{
    public partial class LxIcon
    {
        public const string ClassName = "lx-icon";
        public const string VariableColor = ClassName + "-color";
        private const string AltDefaultValue = "icon";

        private bool _isSourceResource;
        private Uri? _sourceUri;
        private string? _sourceMarkup;
        private Color _fillColor;

        protected override void OnParametersSet()
        {
            _fillColor = StyleContext.GetColorOrDefault(VariableColor, VariableNames.NeutralPrimary);
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Source == null
                || !Uri.TryCreate(Source, UriKind.RelativeOrAbsolute, out _sourceUri))
            {
                _sourceUri = null;
                _sourceMarkup = null;
                _isSourceResource = false;
                return;
            }

            // Check whether the source points to a resource
            _isSourceResource = _sourceUri.Scheme == "rsrc";
            _sourceMarkup = _isSourceResource ? await IconManager.GetMarkupAsync(_sourceUri) : null;
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddAttribute("role", "img");
            builder.AddAriaAttribute("hidden", "true");
            builder.AddClassAttribute(ClassName);

            builder.AddStyleAttribute(x => x
                .AddProperty("height", Size.ToString())
                .AddProperty("min-height", Size.ToString())
                .AddProperty("width", Size.ToString())
                .AddProperty("min-width", Size.ToString())
                .AddProperty("fill", Fill.IsEmpty ? _fillColor.ToRgbStr() : Fill.ToRgbaStr(), _isSourceResource)
                .AddProperty("color", Fill.IsEmpty ? _fillColor.ToRgbStr() : Fill.ToRgbaStr(), _isSourceResource));
        }

        [Inject]
        private IIconManager IconManager { get; set; }

        #region Parameters

        /// <summary>
        /// Gets or sets the height and width of the icon. The default value is 1.5rem.
        /// </summary>
        [Parameter]
        public Dimension Size { get; set; } = new Dimension(1.5, Unit.REM);

        /// <summary>
        /// Gets or sets the source (-url) from which the icon is loaded.
        /// Supports the <c>rsrc</c> uri scheme to load icons from a defined icon source.
        /// </summary>
        /// <remarks>
        /// Icon from default source: <c>rsrc:icon_name</c>
        /// <br/>
        /// Icon from source "my.icon.source": <c>rsrc://my.icon.source/icon_name</c>
        /// </remarks>
        [Parameter]
        public string? Source { get; set; }

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