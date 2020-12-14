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
            var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
            var store = host.Services.GetRequiredService<IJSObjectReferenceStore>();

            await jsRuntime.ImportModuleAsync<IJSInProcessObjectReference>(CookieManager.ModulePath, CookieManager.ModuleKey, store);
        }
    }
}