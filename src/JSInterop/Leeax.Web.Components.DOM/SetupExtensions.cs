using Leeax.Web.Components.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Linq;

namespace Leeax.Web.Components.DOM
{
    public static class SetupExtensions
    {
        public static void AddDomApi(this IServiceCollection services)
        {
            services.AddJSObjectStore();
            services.Add(new ServiceDescriptor(
                typeof(IElementService),
                typeof(ElementService),
                services.First(x => x.ServiceType == typeof(IJSRuntime)).Lifetime));
            services.Add(new ServiceDescriptor(
                typeof(IHeadManager),
                typeof(HeadManager),
                services.First(x => x.ServiceType == typeof(IJSRuntime)).Lifetime));
        }
    }
}