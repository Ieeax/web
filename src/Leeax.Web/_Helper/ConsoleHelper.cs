using Leeax.Web.Internal;
using System;

namespace Leeax.Web
{
    public static class ConsoleHelper
    {
        public static void WriteFromComponent(Type sourceComponent, string? message)
        {
            sourceComponent.ThrowIfNull();

            Console.WriteLine($"[{sourceComponent.FullName}] " + message);
        }

        public static void WriteExceptionFromComponent(Exception exception, Type sourceComponent)
        {
            exception.ThrowIfNull();
            sourceComponent.ThrowIfNull();

            WriteFromComponent(sourceComponent, $"{exception.GetType().FullName}: {exception.Message}");
        }

        public static void WriteException(Exception exception)
        {
            exception.ThrowIfNull();

            Console.WriteLine(exception.GetType().FullName + ": " + exception.Message);
            //Console.WriteLine(exception.StackTrace.Replace("\r", ""));
        }
    }
}