using System;
using System.Runtime.CompilerServices;

namespace Leeax.Web.Builders.Internal
{
    public static class ThrowHelperExensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> when <paramref name="value"/> is null.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowIfNull(this object? value, [CallerMemberName] string? argumentName = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}