using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Input
{
    public class LxInputButton : DefinitionComponent
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the icon source.
        /// </summary>
        [Parameter]
        public string? IconSource { get; set; }

        /// <summary>
        /// Gets or sets the action to execute.
        /// </summary>
        [Parameter]
        public EventCallback Clicked { get; set; }
    }
}