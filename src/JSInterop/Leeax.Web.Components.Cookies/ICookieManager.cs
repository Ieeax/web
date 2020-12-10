using System.Threading.Tasks;

namespace Leeax.Web.Components.Cookies
{
    public interface ICookieManager
    {
        void SetCookie(CookieOptions options);

        ValueTask SetCookieAsync(CookieOptions options);

        string? GetCookie(string name);

        ValueTask<string?> GetCookieAsync(string name);

        void RemoveCookie(string name);

        ValueTask RemoveCookieAsync(string name);
    }
}