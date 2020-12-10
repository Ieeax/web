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
            var store = host.Services.GetService<IJSObjectReferenceStore>();
            if (store != null)
            {
                await store.ImportModuleAsync<IJSInProcessObjectReference>(ElementService.ModuleKey, ElementService.ModulePath);
                await store.ImportModuleAsync<IJSInProcessObjectReference>(HeadManager.ModuleKey, HeadManager.ModulePath);
            }
        }
    }
}