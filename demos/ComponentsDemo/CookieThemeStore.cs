using System;
using System.Threading.Tasks;
using Leeax.Web.Components.Cookies;
using Leeax.Web.Components.Theme;
using System.Text.Json;

namespace ComponentsDemo
{
    public class CookieThemeStore : IThemeStore
    {
        private const string COOKIE_KEY = "__lx-theme";
        private readonly ICookieManager _cookieManager;

        public CookieThemeStore(ICookieManager cookieManager)
        {
            _cookieManager = cookieManager;
        }

        public async ValueTask<ThemeState> ReadAsync()
        {
            var jsonStr = await _cookieManager.GetCookieAsync(COOKIE_KEY);
            if (string.IsNullOrEmpty(jsonStr))
            {
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<ThemeState>(jsonStr);
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public ValueTask WriteAsync(ThemeState state)
        {
            var jsonStr = JsonSerializer.Serialize(state);

            return _cookieManager.SetCookieAsync(
                new CookieOptions(COOKIE_KEY, jsonStr, TimeSpan.FromDays(30))
                {
                    Path = "/"
                });
        }
    }
}