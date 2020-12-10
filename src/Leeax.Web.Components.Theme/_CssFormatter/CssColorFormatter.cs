using System;
using System.Drawing;

namespace Leeax.Web.Components.Theme
{
    public class CssColorFormatter : ICssValueFormatter<Color>
    {
        public string Format(Type valueType, Color value)
            => value.ToRgbaStr();
    }
}