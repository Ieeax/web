using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Leeax.Web.Components.Abstractions
{
    public static class SetupExtensions
    {
        public static void AddJSObjectStore(this IServiceCollection services)
        {
            services.TryAddSingleton<IJSObjectReferenceStore, JsObjectReferenceStore>();
        }
    }
}