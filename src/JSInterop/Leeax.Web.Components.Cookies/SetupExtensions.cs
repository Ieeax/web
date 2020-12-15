using Leeax.Web.Components.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Linq;

namespace Leeax.Web.Components.Cookies
{
    public static class SetupExtensions
    {
        public static void AddCookies(this IServiceCollection services)
        {
            // Determine whether we running on WebAssembly or Server-Side
            var jsRuntimeServiceLifetime = services
                .First(x => x.ServiceType == typeof(IJSRuntime)).Lifetime;

            // Register bootstrapper only when runnning on WebAssembly
            if (jsRuntimeServiceLifetime == ServiceLifetime.Singleton)
            {
                services.AddBootstrapper(new ModuleBootstrapper(CookieManager.Module));
            }

            services.AddJSObjectStore();
            services.Add(new ServiceDescriptor(typeof(ICookieManager), typeof(CookieManager), jsRuntimeServiceLifetime));
        }
    }
}