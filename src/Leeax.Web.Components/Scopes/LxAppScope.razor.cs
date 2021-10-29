using Leeax.Web.Components.Modals;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Leeax.Web.Components.Scopes
{
    public partial class LxAppScope
    {
        private IModalService? _modalService;
        private IToastService? _toastService;

        protected override void OnInitialized()
        {
            _modalService = ServiceProvider.GetService(typeof(IModalService)) as IModalService;
            _toastService = ServiceProvider.GetService(typeof(IToastService)) as IToastService;
        }

        [Inject]
        private IServiceProvider ServiceProvider { get; set; } = null!;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? Attributes { get; set; }
    }
}