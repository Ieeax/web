using Leeax.Web.Components.Modals;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Leeax.Web.Components.Scopes
{
    public partial class LxAppScope
    {
        [Inject]
        public IModalService DialogService { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? Attributes { get; set; }
    }
}