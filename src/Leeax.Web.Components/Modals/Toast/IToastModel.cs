namespace Leeax.Web.Components.Modals
{
    public interface IToastModel : INotifyClosed
    {
        /// <summary>
        /// Gets or sets how long the toast displays.
        /// </summary>
        int DisplayTime { get; }
    }
}