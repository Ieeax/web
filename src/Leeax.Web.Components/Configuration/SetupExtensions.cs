using System;
using System.Linq;
using System.Net.Http;
using Leeax.Web.Components.DOM;
using Leeax.Web.Components.Modals;
using Leeax.Web.Components.Window;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Leeax.Web.Components.Configuration
{
    public static class SetupExtensions
    {
        public static IServiceCollection AddComponents(this IServiceCollection services)
        {
            services.AddDomApi();
            services.AddWindowApi();

            var iconOptions = new IconProviderOptions();
            iconOptions.AddDefaultIcons();

            // Add default "IIconProvider" service to prevent exceptions if the user doesn't manually call "AddIconProvider"
            services.TryAddSingleton<IIconProvider>(new IconProvider(iconOptions));

            return services;
        }

        public static IServiceCollection AddIcons(this IServiceCollection services, Action<IconOptions> configure)
        {
            var options = new IconOptions();

            // Invoke configuration action
            configure?.Invoke(options);

            services.AddScoped<IIconCache>(provider => new SimpleIconCache());
            services.AddScoped<IIconManager>(
                provider => new IconManager(
                    provider.GetRequiredService<HttpClient>(), 
                    provider.GetRequiredService<IIconCache>(), 
                    options));

            return services;
        }

        public static IServiceCollection AddIconProvider(this IServiceCollection services, Action<IconProviderOptions>? configure)
        {
            var options = new IconProviderOptions();
            options.AddDefaultIcons();

            // Invoke configuration action
            configure?.Invoke(options);

            var alreadyExists = services.Any(x => x.ServiceType == typeof(IIconProvider));
            if (alreadyExists)
            {
                return services.Replace(
                    new ServiceDescriptor(typeof(IIconProvider), new IconProvider(options)));
            }
            else
            {
                return services.AddSingleton<IIconProvider>(
                    new IconProvider(options));
            }
        }

        public static IServiceCollection AddModals(this IServiceCollection services, Action<ModalsOptions>? configure = null)
        {
            var options = new ModalsOptions();

            // Add default implementation of "toast"
            options.Toast.AddComponent<DefaultToastModel, LxDefaultToast>();

            // Add default implementation of "MessageBox"
            options.Modal.AddComponent<MessageBoxModel, LxMessageBox>();

            // Invoke configuration action
            configure?.Invoke(options);

            services.AddSingleton<IToastService>(provider => new ToastService(provider, options.Toast));
            services.AddSingleton<IToastRenderService>(provider => (IToastRenderService)provider.GetRequiredService<IToastService>());

            services.AddSingleton<IModalService>(provider => new ModalService(provider, options.Modal));
            services.AddSingleton<IModalRenderService>(provider => (IModalRenderService)provider.GetRequiredService<IModalService>());

            return services;
        }
    }
}