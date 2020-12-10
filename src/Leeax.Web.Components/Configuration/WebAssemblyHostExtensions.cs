using Leeax.Web.Components.DOM;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Leeax.Web.Components.Configuration
{
    public static class WebAssemblyHostExtensions
    {
        public static WebAssemblyHost AddStaticAssets(this WebAssemblyHost host)
        {
            var service = host.Services.GetService<IHeadManager>();

            service?.ApplyLink("_content/Leeax.Web.Components/bootstrap.min.css", "stylesheet");
            service?.ApplyLink("_content/Leeax.Web.Components/global.min.css", "stylesheet");

            return host;
        }
    }
}