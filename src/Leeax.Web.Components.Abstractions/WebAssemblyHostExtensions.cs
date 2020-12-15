using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Abstractions
{
    public static class WebAssemblyHostExtensions
    {
        public static Task RunWithBootstrappersAsync(this WebAssemblyHost host)
        {
            return BootstrapperUtilities
                .RunFromServiceProviderAsync(host.Services)
                .ContinueWith(x => host.RunAsync());
        }
    }
}