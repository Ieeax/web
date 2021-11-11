using Leeax.Web.Components.Theme;
using System.Drawing;

namespace Leeax.Web.Components
{
    public class DefaultLightThemeStyle : StyleBase
    {
        public override void BuildStyle(StyleBuilder builder)
        {
            builder.Add(VariableNames.ThemeDarker, ColorHelper.FromHexStr("004578"));
            builder.Add(VariableNames.ThemeDark, ColorHelper.FromHexStr("005a9e"));
            builder.Add(VariableNames.ThemePrimary, ColorHelper.FromHexStr("0078d4"));
            builder.Add(VariableNames.ThemeSecondary, ColorHelper.FromHexStr("2b88d8"));
            builder.Add(VariableNames.ThemeTertiary, ColorHelper.FromHexStr("71afe5"));
            builder.Add(VariableNames.ThemeLight, ColorHelper.FromHexStr("c7e0f4"));
            builder.Add(VariableNames.ThemeLighter, ColorHelper.FromHexStr("deecf9"));
            builder.Add(VariableNames.NeutralBlack, Color.Black);
            builder.Add(VariableNames.NeutralDark, ColorHelper.FromHexStr("1b1d20"));
            builder.Add(VariableNames.NeutralPrimary, ColorHelper.FromHexStr("2f3136"));
            builder.Add(VariableNames.NeutralSecondary, ColorHelper.FromHexStr("51555e"));
            builder.Add(VariableNames.NeutralTertiary, ColorHelper.FromHexStr("82868f"));
            builder.Add(VariableNames.NeutralWhite, Color.White);
            builder.Add(VariableNames.NeutralQuaternary, ColorHelper.FromHexStr("c4c7cb"));
            builder.Add(VariableNames.NeutralQuaternaryAlt, ColorHelper.FromHexStr("d6d9dd"));
            builder.Add(VariableNames.NeutralLight, ColorHelper.FromHexStr("e9ebed"));
            builder.Add(VariableNames.NeutralLighter, ColorHelper.FromHexStr("f6f6f7"));
            builder.Add(VariableNames.ElevationColor, "100, 106, 119");
            builder.Add(VariableNames.ModalBackground, Color.White);
        }
    }
}