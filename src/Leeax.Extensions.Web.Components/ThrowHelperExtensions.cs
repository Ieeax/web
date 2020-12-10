using System;
using System.Runtime.CompilerServices;

namespace Leeax.Extensions.Web.Components
{
    internal static class ThrowHelperExensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> when <paramref name="value"/> is null.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        internal static void ThrowIfNull(this object? value, [CallerMemberName] string? argumentName = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}