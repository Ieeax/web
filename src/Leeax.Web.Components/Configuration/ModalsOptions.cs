namespace Leeax.Web.Components.Configuration
{
    public class ModalsOptions
    {
        public ModalsOptions()
        {
            Toast = new ToastOptions();
            Modal = new ModalOptions();
        }

        public ToastOptions Toast { get; }

        public ModalOptions Modal { get; set; }
    }
}