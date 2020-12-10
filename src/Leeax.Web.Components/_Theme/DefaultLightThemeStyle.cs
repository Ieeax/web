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
            builder.Add(VariableNames.NeutralDark, ColorHelper.FromHexStr("201f1e"));
            builder.Add(VariableNames.NeutralPrimary, ColorHelper.FromHexStr("323130"));
            builder.Add(VariableNames.NeutralSecondary, ColorHelper.FromHexStr("605e5c"));
            builder.Add(VariableNames.NeutralTertiary, ColorHelper.FromHexStr("a19f9d"));
            builder.Add(VariableNames.NeutralWhite, Color.White);
            builder.Add(VariableNames.NeutralQuaternaryAlt, ColorHelper.FromHexStr("e1dfdd"));
            builder.Add(VariableNames.NeutralQuaternary, ColorHelper.FromHexStr("d0d0d0"));
            builder.Add(VariableNames.NeutralLight, ColorHelper.FromHexStr("edebe9"));
            builder.Add(VariableNames.NeutralLighter, ColorHelper.FromHexStr("f3f2f1"));
            builder.Add("lx-color-elevation", "100, 106, 119");
            builder.Add("lx-modal-background", Color.White);
        }
    }
}