using System;
using System.Collections.Generic;

namespace Leeax.Web.Components.Theme
{
    public interface IThemeHandler
    {
        event EventHandler? ThemeChanged;

        StyleBase GetActiveStyle();

        Dictionary<string, StyleBase> Themes { get; }

        string ActiveTheme { get; set; }
    }
}