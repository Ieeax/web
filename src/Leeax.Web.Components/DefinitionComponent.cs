using Microsoft.AspNetCore.Components;
using System;

namespace Leeax.Web.Components
{
    public class DefinitionComponent : ComponentBase, IDisposable
    {
        protected override void OnInitialized()
        {
            Parent?.AddChild(this);
        }

        public virtual void Dispose()
        {
            Parent?.RemoveChild(this);
        }

        [CascadingParameter(Name = nameof(Parent))]
        public IContext? Parent { get; set; }
    }
}