using Leeax.Web.Components.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Linq;

namespace Leeax.Web.Components.Window
{
    public static class SetupExtensions
    {
        public static void AddWindowApi(this IServiceCollection services)
        {
            services.AddJSObjectStore();
            services.Add(new ServiceDescriptor(
                typeof(IWindowService),
                typeof(WindowService),
                services.First(x => x.ServiceType == typeof(IJSRuntime)).Lifetime));
        }
    }
}