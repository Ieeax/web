using Leeax.Web.Components.Theme;

namespace Leeax.Web.Components.Modals
{
    internal class ToastButtonStyle : StyleBase
    {
        public override void BuildStyle(StyleBuilder builder)
        {
            builder.AddAlias("lx-button-background", "lx-color-theme-primary");
        }

        public static ToastButtonStyle Instance { get; } = new ToastButtonStyle();
    }
}