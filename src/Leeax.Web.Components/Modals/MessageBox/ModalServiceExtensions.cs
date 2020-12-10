using System.Threading.Tasks;

namespace Leeax.Web.Components.Modals
{
    public static class ModalServiceExtensions
    {
        public static Task<DialogResult> ShowMessageAsync(this IModalService service, string? text, MessageBoxButtons buttons) 
            => ShowMessageAsync(service, null, text, buttons);

        public static async Task<DialogResult> ShowMessageAsync(this IModalService service, string? title, string? text, MessageBoxButtons buttons)
        {
            var model = new MessageBoxModel()
            {
                Title = title,
                Text = text,
                Buttons = buttons
            };

            await service.ShowAsync(model);

            return model.DialogResult;
        }
    }
}