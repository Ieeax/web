using Leeax.Web.Components.DOM;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;

namespace Leeax.Web.Components.Theme
{
    public static class SetupExtensions
    {
        public static void AddTheming(this IServiceCollection services, Action<ThemeOptions>? configure)
        {
            var options = new ThemeOptions();

            // Add default formatters
            options.CssValueFormatters.Add(typeof(KeyAlias), new CssKeyAliasFormatter());
            options.CssValueFormatters.Add(typeof(Color), new CssColorFormatter());

            configure?.Invoke(options);

            services.AddSingleton<IStyleScopeHandler>(
                sp => new StyleScopeHandler(sp.GetRequiredService<IHeadManager>(), options.CssValueFormatters));

            services.AddSingleton<IThemeHandler>(
                sp => new ThemeHandler(sp, options));
        }
    }
}