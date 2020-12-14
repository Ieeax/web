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
            var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
            var store = host.Services.GetRequiredService<IJSObjectReferenceStore>();

            await jsRuntime.ImportModuleAsync<IJSInProcessObjectReference>(WindowService.ModulePath, WindowService.ModuleKey, store);
        }
    }
}