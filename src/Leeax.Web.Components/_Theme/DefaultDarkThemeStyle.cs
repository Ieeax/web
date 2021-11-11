using Leeax.Web.Components.Theme;

namespace Leeax.Web.Components
{
    public class DefaultDarkThemeStyle : StyleBase
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
            builder.Add(VariableNames.NeutralBlack, ColorHelper.FromHexStr("f0f1f7"));
            builder.Add(VariableNames.NeutralDark, ColorHelper.FromHexStr("eaebef"));
            builder.Add(VariableNames.NeutralPrimary, ColorHelper.FromHexStr("fff"));
            builder.Add(VariableNames.NeutralSecondary, ColorHelper.FromHexStr("d6d8df"));
            builder.Add(VariableNames.NeutralTertiary, ColorHelper.FromHexStr("acafb9"));
            builder.Add(VariableNames.NeutralWhite, ColorHelper.FromHexStr("1f2022"));
            builder.Add(VariableNames.NeutralQuaternary, ColorHelper.FromHexStr("494b50"));            
            builder.Add(VariableNames.NeutralQuaternaryAlt, ColorHelper.FromHexStr("404246"));
            builder.Add(VariableNames.NeutralLight, ColorHelper.FromHexStr("36373a"));
            builder.Add(VariableNames.NeutralLighter, ColorHelper.FromHexStr("2f3032"));
            builder.Add(VariableNames.ElevationColor, "0, 0, 0");
            builder.Add(VariableNames.ModalBackground, ColorHelper.FromHexStr("333538"));
        }
    }
}
