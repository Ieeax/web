using Microsoft.AspNetCore.Components;

namespace ComponentsDemo.Components
{
    public partial class Entry
    {
        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}