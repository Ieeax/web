using System;

namespace Leeax.Web.Components
{
    [Flags]
    public enum ResizeAxis
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
        Both = ResizeAxis.Horizontal | ResizeAxis.Vertical
    }
}