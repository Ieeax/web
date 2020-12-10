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
            builder.Add(VariableNames.NeutralBlack, ColorHelper.FromHexStr("f8f8f8"));
            builder.Add(VariableNames.NeutralDark, ColorHelper.FromHexStr("f4f4f4"));
            builder.Add(VariableNames.NeutralPrimary, ColorHelper.FromHexStr("fff"));
            builder.Add(VariableNames.NeutralSecondary, ColorHelper.FromHexStr("d0d0d0"));
            builder.Add(VariableNames.NeutralTertiary, ColorHelper.FromHexStr("c8c8c8"));
            builder.Add(VariableNames.NeutralWhite, ColorHelper.FromHexStr("1f1f1f"));
            builder.Add(VariableNames.NeutralQuaternaryAlt, ColorHelper.FromHexStr("484848"));
            builder.Add(VariableNames.NeutralQuaternary, ColorHelper.FromHexStr("4f4f4f"));
            builder.Add(VariableNames.NeutralLight, ColorHelper.FromHexStr("3f3f3f"));
            builder.Add(VariableNames.NeutralLighter, ColorHelper.FromHexStr("313131"));
            builder.Add("lx-color-elevation", "0, 0, 0");
            builder.Add("lx-modal-background", ColorHelper.FromHexStr("313131"));
        }
    }
}
