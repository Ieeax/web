using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Theme
{
    public class ThemeHandler : IThemeHandler
    {
        private readonly ThemeOptions _options;
        private readonly IThemeStore? _store;
        private string _activeTheme;

        public event EventHandler? ThemeChanged;

        public ThemeHandler(IServiceProvider serviceProvider, ThemeOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _store = options.ThemeStoreFactory?.Invoke(serviceProvider);
            _activeTheme = options.InitialTheme ?? ThemeTypes.Light;

            LoadFromStoreAsync();
        }

        private async Task LoadFromStoreAsync()
        {
            if (_store == null) return;

            try
            {
                var lastThemeState = await _store.ReadAsync();
                if (lastThemeState != null
                    && !string.IsNullOrEmpty(lastThemeState.Name))
                {
                    ActiveTheme = lastThemeState.Name;
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteException(ex);
            }
        }

        private async Task SaveToStoreAsync()
        {
            if (_store == null) return;

            try
            {
                await _store.WriteAsync(new ThemeState()
                {
                    Name = ActiveTheme
                });
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteException(ex);
            }
        }

        public StyleBase GetActiveStyle()
        {
            return _options.Themes.TryGetValue(ActiveTheme, out var value)
                ? value
                : _options.FallbackTheme != null 
                    && _options.Themes.TryGetValue(_options.FallbackTheme, out value)
                    ? value
                    : StyleBase.Empty;
        }

        public Dictionary<string, StyleBase> Themes => _options.Themes;

        public string ActiveTheme
        { 
            get => _activeTheme; 
            set
            {
                if (_activeTheme != value)
                {
                    _activeTheme = value ?? throw new ApplicationException($"Property \"{nameof(ActiveTheme)}\" cannot be null.");
                    ThemeChanged?.Invoke(this, EventArgs.Empty);

                    SaveToStoreAsync();
                }
            }
        }
    }
}