using System;
using System.Collections.Generic;

namespace Leeax.Web.Components.Theme
{
    public class ThemeOptions
    {
        public Dictionary<Type, ICssValueFormatter> CssValueFormatters { get; } = new Dictionary<Type, ICssValueFormatter>();

        public Func<IServiceProvider, IThemeStore>? ThemeStoreFactory { get; set; }

        public Dictionary<string, StyleBase> Themes { get; } = new Dictionary<string, StyleBase>();

        public string? InitialTheme { get; set; } = ThemeTypes.Light;

        public string? FallbackTheme { get; set; } = ThemeTypes.Light;
    }
}