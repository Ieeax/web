using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Input
{
    public class LxInputIcon : DefinitionComponent
    {
        /// <summary>
        /// Gets or sets the icon source.
        /// </summary>
        [Parameter]
        public string? Source { get; set; }
    }
}