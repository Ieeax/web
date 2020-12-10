using Leeax.Web.Internal;
using System;

namespace Leeax.Web
{
    public static class UnitHelper
    {
        public static string ToString(Unit unit)
        {
            return unit switch
            {
                Unit.Pixel => "px",
                Unit.Percent => "%",
                Unit.EM => "em",
                Unit.REM => "rem",
                Unit.ViewportWidth => "vw",
                Unit.ViewportHeight => "vh",
                _ => throw new NotImplementedException(),
            };
        }

        public static Unit ToUnit(string unitStr)
        {
            unitStr.ThrowIfNull();

            return unitStr switch
            {
                "px" => Unit.Pixel,
                "%" => Unit.Percent,
                "em" => Unit.EM,
                "rem" => Unit.REM,
                "vw" => Unit.ViewportWidth,
                "vh" => Unit.ViewportHeight,
                _ => Unit.Pixel,
            };
        }
    }
}