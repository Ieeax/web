using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Navigation
{
    public class LxTab : DefinitionComponent
    {
        /// <summary>
        /// Gets or sets the key. Should not be null.
        /// </summary>
        [Parameter]
        public string? Key { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}