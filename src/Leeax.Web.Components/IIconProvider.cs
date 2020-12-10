using System.Collections.Generic;
using System.Drawing;

namespace Leeax.Web.Components
{
    public interface IIconProvider
    {
        string? Resolve(string key, Color fillColor);

        string? Resolve(string key, string fillColor);

        bool TryResolve(string key, Color fillColor, out string? source);

        bool TryResolve(string key, string fillColor, out string? source);

        IEnumerable<string> IconCollection { get; }
    }
}