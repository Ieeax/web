using Microsoft.AspNetCore.Components;

namespace ComponentsDemo.Components
{
    public partial class Demo
    {
        [Parameter]
        public RenderFragment Content { get; set; }

        [Parameter]
        public RenderFragment Customization { get; set; }
    }
}