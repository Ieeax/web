using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using System.Drawing;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Modals
{
    public partial class LxDefaultToast : IModelComponent<DefaultToastModel>
    {
        public const string ClassName = "lx-toast-default";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, ClassNames.FlexRow, "p-2")
                .Add("stacked", Model != null && Model.Stacked));
        }

        protected MarkupString GetIconMarkup(Color color)
        {
            var hexCode = color.ToHexStr();

            return (MarkupString)(Model!.Icon switch
            {
                ToastIcon.Info => "<svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" viewBox=\"0 0 172 172\"><g fill=\"none\" fill-rule=\"nonzero\" stroke=\"none\" stroke-width=\"1\" stroke-linecap=\"butt\" stroke-linejoin=\"miter\" stroke-miterlimit=\"10\" stroke-dasharray=\"\" stroke-dashoffset=\"0\" style=\"mix-blend-mode: normal\"><path d=\"M0,172v-172h172v172z\" fill=\"none\"></path><g fill=\"" + hexCode + "\"><path d=\"M86,16.125c-38.52783,0 -69.875,31.34717 -69.875,69.875c0,38.52783 31.34717,69.875 69.875,69.875c38.52783,0 69.875,-31.34717 69.875,-69.875c0,-38.52783 -31.34717,-69.875 -69.875,-69.875zM86,26.875c32.71192,0 59.125,26.41309 59.125,59.125c0,32.71192 -26.41308,59.125 -59.125,59.125c-32.71191,0 -59.125,-26.41308 -59.125,-59.125c0,-32.71191 26.41309,-59.125 59.125,-59.125zM80.625,53.75v10.75h10.75v-10.75zM80.625,75.25v43h10.75v-43z\"></path></g></g></svg>",
                ToastIcon.Warning => "<svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" viewBox=\"0 0 172 172\"><g fill=\"none\" fill-rule=\"nonzero\" stroke=\"none\" stroke-width=\"1\" stroke-linecap=\"butt\" stroke-linejoin=\"miter\" stroke-miterlimit=\"10\" stroke-dasharray=\"\" stroke-dashoffset=\"0\" style=\"mix-blend-mode: normal\"><path d=\"M0,172v-172h172v172z\" fill=\"none\"></path><g fill=\"" + hexCode + "\"><path d=\"M86,17.30078l-4.70312,8.0625l-64.5,111.69922l-4.53516,8.0625h147.47656l-4.53516,-8.0625l-64.5,-111.69922zM86,38.80078l55.09375,95.57422h-110.1875zM80.625,75.25v32.25h10.75v-32.25zM80.625,112.875v10.75h10.75v-10.75z\"></path></g></g></svg>",
                ToastIcon.Error => "<svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" viewBox=\"0 0 32 32\"><g fill=\"" + hexCode + "\" id=\"surface1\"><path d=\"M 7.21875 5.78125 L 5.78125 7.21875 L 14.5625 16 L 5.78125 24.78125 L 7.21875 26.21875 L 16 17.4375 L 24.78125 26.21875 L 26.21875 24.78125 L 17.4375 16 L 26.21875 7.21875 L 24.78125 5.78125 L 16 14.5625 Z \"/></g></svg>",
                ToastIcon.Success => "<svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" viewBox=\"0 0 172 172\"><g fill=\"none\" fill-rule=\"nonzero\" stroke=\"none\" stroke-width=\"1\" stroke-linecap=\"butt\" stroke-linejoin=\"miter\" stroke-miterlimit=\"10\" stroke-dasharray=\"\" stroke-dashoffset=\"0\" style=\"mix-blend-mode: normal\"><path d=\"M0,172v-172h172v172z\" fill=\"none\"></path><g fill=\"" + hexCode + "\"><path d=\"M152.01172,33.76172l-92.88672,92.88672l-39.13672,-39.13672l-7.72656,7.72656l43,43l3.86328,3.69531l3.86328,-3.69531l96.75,-96.75z\"></path></g></g></svg>",
                _ => string.Empty,
            });
        }

        protected Color GetIconColor()
        {
            return Model!.Icon switch
            {
                ToastIcon.Warning => ColorHelper.FromHexStr("ffc800"),
                ToastIcon.Error => Color.Red,
                ToastIcon.Success => ColorHelper.FromHexStr("2ac666"),
                _ => StyleContext.GetColorOrDefault(VariableNames.ThemePrimary, default),
            };
        }

        protected void ExecuteActionForButton(ToastButton button)
        {
            if (Model == null
                || button?.Command == null)
            {
                return;
            }

            button.Command(Model.GetToastContext());
        }

        [Parameter]
        public DefaultToastModel? Model { get; set; }
    }
}