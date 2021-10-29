using System;

namespace Leeax.Web
{
    public static class UnitHelper
    {
        public static string Format(Unit value)
        {
            return value switch
            {
                Unit.Pixel => "px",
                Unit.Percent => "%",
                Unit.EM => "em",
                Unit.REM => "rem",
                Unit.ViewportWidth => "vw",
                Unit.ViewportHeight => "vh",
                _ => throw new ArgumentOutOfRangeException(nameof(value)),
            };
        }

        public static bool TryParse(string? value, out Unit result)
        {
            result = value switch
            {
                "px" => Unit.Pixel,
                "%" => Unit.Percent,
                "em" => Unit.EM,
                "rem" => Unit.REM,
                "vw" => Unit.ViewportWidth,
                "vh" => Unit.ViewportHeight,
                _ => Unit.Pixel
            };
            
            return result != Unit.Pixel || value == "px";
        }
    }
}