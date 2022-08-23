using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Leeax.Web.Components.Modals;
using Leeax.Web.Components.Configuration;
using Leeax.Web.Components.Clipboard;
using Leeax.Web.Components.Cookies;
using Leeax.Web.Components.Theme;
using Leeax.Web.Components;

namespace ComponentsDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            //builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Add all basic services of the library
            builder.Services.AddComponents();

            // Add icons from different sources
            // -> Optional, when not registered icons won't be displayed
            builder.Services.AddIcons(opt =>
            {
                // Add icons used in components of the library
                opt.AddSymbolFile("leeax.web.components", "./_content/Leeax.Web.Components/icons/symbols.svg");

                // Add the "heroicons" icons
                opt.AddDirectory("tailwind.hero", "https://raw.githubusercontent.com/tailwindlabs/heroicons/master/optimized/24/solid/");

                // Add the "Material Design" icons
                opt.AddSymbolFile("google.materialdesign", "./materialdesign-symbols.min.svg");

                // Add the "Material Design" icons as default icon source
                //opt.AddSymbolFile("./materialdesign-symbols.min.svg");
            });

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
            builder.Services.AddClipboard();
            builder.Services.AddCookies();

            // Run bootstrappers and then the app
            // -> Bootstrappers are required for some jsinterop libraries
            await builder.Build()
                .RunWithBootstrappersAsync();
        }
    }
}