using System.Threading.Tasks;

namespace Leeax.Web.Components.Modals
{
    public static class ModalServiceExtensions
    {
        public static Task<DialogResult> ShowMessageAsync(this IModalService service, string? text, MessageBoxButtons buttons, bool requireInteraction = true) 
            => ShowMessageAsync(service, null, text, buttons, requireInteraction);

        public static async Task<DialogResult> ShowMessageAsync(this IModalService service, string? title, string? text, MessageBoxButtons buttons, bool requireInteraction = true)
        {
            var model = new MessageBoxModel()
            {
                Title = title,
                Text = text,
                Buttons = buttons,
                RequireInteraction = requireInteraction
            };

            await service.ShowAsync(model);

            return model.DialogResult;
        }
    }
}