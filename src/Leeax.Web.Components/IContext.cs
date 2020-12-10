using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components
{
    public interface IContext
    {
        void AddChild(ComponentBase component);

        void RemoveChild(ComponentBase component);
    }
}