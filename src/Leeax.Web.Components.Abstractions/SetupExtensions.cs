using Leeax.Web.Components.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Leeax.Web.Components.Configuration
{
    public static class SetupExtensions
    {
        public static IServiceCollection AddJSObjectStore(this IServiceCollection services)
        {
            services.TryAddSingleton<IJSObjectReferenceStore, JSObjectReferenceStore>();

            return services;
        }

        public static IServiceCollection AddBootstrapper(this IServiceCollection services, IBootstrapper bootstrapper)
        {
            _ = bootstrapper ?? throw new ArgumentNullException(nameof(bootstrapper));

            services.AddSingleton<IBootstrapper>(bootstrapper);

            return services;
        }
    }
}