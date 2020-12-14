using Leeax.Web.Components.Abstractions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Leeax.Web.Components.DOM
{
    public static class WebAssemblyHostExtensions
    {
        public static async Task ImportDomModulesAsync(this WebAssemblyHost host)
        {
            var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
            var store = host.Services.GetRequiredService<IJSObjectReferenceStore>();

            await jsRuntime.ImportModuleAsync<IJSInProcessObjectReference>(ElementService.ModulePath, ElementService.ModuleKey, store);
            await jsRuntime.ImportModuleAsync<IJSInProcessObjectReference>(HeadManager.ModulePath, HeadManager.ModuleKey, store);
        }
    }
}