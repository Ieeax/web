# Leeax.Web.Components 
Boilerplate with basic components, controls and functionality for Blazor.

> **Attention**: This project is currently optimized for _Blazor WebAssembly_, so _Server-Side_ may not work correctly.

## Dependencies
This project as well all dependencies are based on .NET 5.0.
- Leeax.Web
- Leeax.Web.Builders
- Leeax.Extensions.Web.Builders
- Leeax.Web.Components.Abstractions
- Leeax.Web.Components.Theme
- Leeax.Web.Components.Transition
- Leeax.Web.Components.Window
- Leeax.Web.Components.DOM

## Getting Started

**Let's get started!**<br>
Add the following code to your application:

**index.html**
```html
<link href="_content/Leeax.Web.Components/bootstrap.min.css" rel="stylesheet" />
<link href="_content/Leeax.Web.Components/global.min.css" rel="stylesheet" />
<link href="https://fonts.googleapis.com/css?family=Roboto:400,500,700&display=swap" rel="stylesheet" />

<!-- It's best to load this after the javascript from blazor itself -->
<script src="_content/Leeax.Web.Components/jsinterop.min.js"></script>
```
The stylesheet _"bootstrap.min.css"_ contains all required classes of bootstrap (around 5kb), if your using bootstrap already this file can be omitted.

**Program.cs**
```csharp
using Leeax.Web.Components.Configuration;
using Leeax.Web.Components.Modals;

...

public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    
    ...
    
    // Add all basic services of the library
    builder.Services.AddComponents();
    
    // Add support for modals (e.g. modal, toast)
    builder.Services.AddModals(opt =>
    {
        opt.Toast.Position = ToastPosition.UpperRight; // Define the toast position
    });
    
    // Add support for theming with two basic themes
    builder.Services.AddTheming(opt =>
    {
        opt.Themes.Add(ThemeTypes.Light, new LightThemeStyle());
        opt.Themes.Add(ThemeTypes.Dark, new DarkThemeStyle());
        opt.InitialTheme = ThemeTypes.Light;
    });
    
    // Run bootstrappers and then the app
    // -> Bootstrappers are required for some jsinterop libraries
    await builder.Build()
        .RunWithBootstrappersAsync();
}
```

**_Imports.cs**
```csharp
@using Leeax.Web
@using Leeax.Web.Components
@using Leeax.Web.Components.Scopes
@using Leeax.Web.Components.Input
@using Leeax.Web.Components.Navigation
@using Leeax.Web.Components.Presentation
@using Leeax.Web.Components.Modals
```
Adding these usings is optional. If not added required usings need to be manually added in each Razor-component.

**App.cs**
```html
<LxAppScope> @* Defines scope of the app *@
    <Router AppAssembly="@typeof(Program).Assembly">
        <Found Context="routeData">
            <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
        </Found>
        <NotFound>
            <LayoutView Layout="@typeof(MainLayout)">
                <p>Sorry, there's nothing at this address.</p>
            </LayoutView>
        </NotFound>
    </Router>
</LxAppScope>
```
The _LxAppScope_ component defines the scope of the application. It creates the root scope for the theme (_LxThemeScope_) and also creates renderers for the different modals (e.g. toast). Preferably rendered above everything else, but doesn't have to be. (Has to be above any component from the library)

**Done!** Your ready to go ...

Feel free to create an issue when encountering any problems.