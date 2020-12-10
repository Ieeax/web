using System.Threading.Tasks;

namespace Leeax.Web.Components.Theme
{
    public interface IThemeStore
    {
        ValueTask<ThemeState?> ReadAsync();

        ValueTask WriteAsync(ThemeState value);
    }
}