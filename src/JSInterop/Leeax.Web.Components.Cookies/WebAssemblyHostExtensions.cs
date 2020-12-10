using Leeax.Web.Components.Abstractions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Cookies
{
    public static class WebAssemblyHostExtensions
    {
        public static async Task ImportCookieModulesAsync(this WebAssemblyHost host)
        {
            var store = host.Services.GetService<IJSObjectReferenceStore>();
            if (store != null)
            {
                await store.ImportModuleAsync<IJSInProcessObjectReference>(CookieManager.ModuleKey, CookieManager.ModulePath);
            }
        }
    }
}