using Leeax.Web.Components.Abstractions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Window
{
    public static class WebAssemblyHostExtensions
    {
        public static async Task ImportWindowModulesAsync(this WebAssemblyHost host)
        {
            var store = host.Services.GetService<IJSObjectReferenceStore>();
            if (store != null)
            {
                await store.ImportModuleAsync<IJSInProcessObjectReference>(WindowService.ModuleKey, WindowService.ModulePath);
            }
        }
    }
}