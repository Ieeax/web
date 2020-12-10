using System;

namespace Leeax.Web.Components.Input
{
    public readonly struct CalendarDay
    {
        public CalendarDay(DateTime date, bool isFocused)
        {
            Date = date;
            IsFocused = isFocused;
        }

        public bool IsFocused { get; }

        public DateTime Date { get; }

        public int Day => Date.Day;

        public int Month => Date.Month;

        public int Year => Date.Year;
    }
}