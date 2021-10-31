using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Leeax.Web.Components.Clipboard
{
    public static class SetupExtensions
    {
        /// <summary>
        /// Adds all services required for using <see cref="IClipboard"/>.
        /// </summary>
        public static void AddClipboard(this IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(
                typeof(IClipboard),
                typeof(Clipboard),
                services.First(x => x.ServiceType == typeof(IJSRuntime)).Lifetime));
        }
    }
}