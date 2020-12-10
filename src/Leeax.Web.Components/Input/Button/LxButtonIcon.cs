using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Input
{
    public class LxButtonIcon : DefinitionComponent
    {
        /// <summary>
        /// Gets or sets the icon source.
        /// </summary>
        [Parameter]
        public string? Source { get; set; }

        /// <summary>
        /// Gets or sets whether the icon should be at the end of the button.
        /// </summary>
        [Parameter]
        public bool Trailing { get; set; }
    }
}