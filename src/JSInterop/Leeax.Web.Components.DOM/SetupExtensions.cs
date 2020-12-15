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
            // Determine whether we running on WebAssembly or Server-Side
            var jsRuntimeServiceLifetime = services
                .First(x => x.ServiceType == typeof(IJSRuntime)).Lifetime;

            // Register bootstrapper only when runnning on WebAssembly
            if (jsRuntimeServiceLifetime == ServiceLifetime.Singleton)
            {
                services.AddBootstrapper(new ModuleBootstrapper(ElementService.Module, HeadManager.Module));
            }

            services.AddJSObjectStore();
            services.Add(new ServiceDescriptor(typeof(IElementService), typeof(ElementService), jsRuntimeServiceLifetime));
            services.Add(new ServiceDescriptor(typeof(IHeadManager), typeof(HeadManager), jsRuntimeServiceLifetime));
        }
    }
}