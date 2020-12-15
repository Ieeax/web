using Leeax.Web.Components.Abstractions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Configuration
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