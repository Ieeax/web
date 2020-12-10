using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Linq;

namespace Leeax.Web.Components.History
{
    public static class SetupExtensions
    {
        public static void AddHistory(this IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(
                typeof(IHistoryManager),
                typeof(HistoryManager),
                services.First(x => x.ServiceType == typeof(IJSRuntime)).Lifetime));
        }
    }
}