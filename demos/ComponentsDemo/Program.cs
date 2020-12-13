using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Leeax.Icons.MaterialDesign;
using Leeax.Web.Components.Modals;
using Leeax.Web.Components.Configuration;
using Leeax.Web.Components.DOM;
using Leeax.Web.Components.Window;
using Leeax.Web.Components.Cookies;
using Leeax.Web.Components;
using Leeax.Web.Components.Theme;

namespace ComponentsDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Add all basic services of the library
            builder.Services.AddComponents();

            // Add the "Material Design" icon-pack
            // -> Is optional, but when not registered icons won't be displayed
            builder.Services.AddIconProvider(opt => opt.AddMaterialDesignIcons());

            // Configure the different modals
            builder.Services.AddModals(opt =>
            {
                opt.Toast.Position = ToastPosition.UpperRight;
            });

            // Configure themes and define where to store the selected one
            builder.Services.AddTheming(opt =>
            {
                opt.ThemeStoreFactory = sp => new CookieThemeStore(sp.GetRequiredService<ICookieManager>());
                opt.Themes.Add(ThemeTypes.Light, new DefaultLightThemeStyle());
                opt.Themes.Add(ThemeTypes.Dark, new DefaultDarkThemeStyle());
            });

            // Add additional js-interop libraries
            builder.Services.AddCookies();

            var host = builder.Build();

            // Import all ES6 modules
            await Task.WhenAll(
                host.ImportDomModulesAsync(),
                host.ImportWindowModulesAsync(),
                host.ImportCookieModulesAsync());

            // Add statis assets like CSS
            // -> It's recommended to add the assets manually for production (in "index.html")
            host.AddStaticAssets();

            await host.RunAsync();
        }
    }
}