using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Leeax.Web.Components
{
    public static class PropertyChangedEventHandlerExtensions
    {
        public static void Raise(this PropertyChangedEventHandler? handler, object? sender, [CallerMemberName] string? memberName = null)
        {
            handler?.Invoke(sender, new PropertyChangedEventArgs(memberName));
        }
    }
}