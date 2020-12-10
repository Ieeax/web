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
            services.AddJSObjectStore();
            services.Add(new ServiceDescriptor(
                typeof(ICookieManager),
                typeof(CookieManager),
                services.First(x => x.ServiceType == typeof(IJSRuntime)).Lifetime));
        }
    }
}