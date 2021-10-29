# Introduction 
This repository consists of several libraries - written mostly in C# and TypeScript/JavaScript - which are targeting the web. The main focus is currently on Blazor (Microsofts new SPA-Framework), which enables the execution of C#-Code in the browser (e.g. through WebAssembly).

# Demo
A demo for most components and features can be found <ins>[here](https://Ieeax.github.io/web/)</ins>. 

# Getting Started
To use one or more of these libraries in your application, simply add a reference to the respective [NuGet-package(-s)](https://www.nuget.org/packages?q=Leeax.Web) and you're ready to go. For more information to a specific project, click on of the links further down.

> **Note**: The projects are currently in active development and may see breaking changes in the future. The current state could be considered as a beta version and is not suitable for production.

All projects are based on .NET 5.0 and can be splited in the following three categories:

#### Web
Libraries which contains basic functionality and helpers for the web.
- Leeax.Web
- [Leeax.Web.Builders](docs/web-builders/GettingStarted.md)

#### Blazor
Boilerplate with basic components, controls and functionality for blazor.
> **Attention**: These projects are currently optimized for _Blazor WebAssembly_, so _Server-Side_ may not work correctly.
- [Leeax.Web.Components](docs/web-components/GettingStarted.md)
- [Leeax.Web.Components.Transition](docs/web-components-transition/GettingStarted.md)
- Leeax.Web.Components.Theme

#### Blazor JSInterop
Wrappers for native JavaScript functionality, easily useable trough services.
- Leeax.Web.Components.Cookies
- Leeax.Web.Components.DOM
- Leeax.Web.Components.History
- Leeax.Web.Components.Window

# Documentation
The current documentation is very limited. More will come in the future.

# Issues
As mentioned, the projects are currently not 100% stable and may contain bugs.<br>
When encountering any bugs/problems feel free to create an issue.
