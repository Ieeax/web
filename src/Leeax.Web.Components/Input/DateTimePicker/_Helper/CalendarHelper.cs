using System;
using System.Collections.Generic;
using System.Globalization;

namespace Leeax.Web.Components.Input
{
    public class CalendarHelper
    {
        /// <summary>
        /// Gets the the current week from the specfied date. (ISO8601)
        /// </summary>
        public static int GetWeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Gets the first date of the specified week/year. (ISO8601)
        /// </summary>
        public static DateTime GetFirstDateOfWeek(int weekOfYear, int year)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }

        /// <summary>
        /// Gets the first date of the specified date.
        /// </summary>
        public static DateTime GetFirstDateOfWeek(DateTime date)
        {
            var diff = date.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }

            return date.AddDays(-diff).Date;
        }

        /// <summary>
        /// Gets the last date of the specified date.
        /// </summary>
        public static DateTime GetLastDateOfWeek(DateTime date) => GetFirstDateOfWeek(date).AddDays(6);

        /// <summary>
        /// Gets the count of weeks in the specified year.
        /// </summary>
        public static int GetCountWeeksInYear(int year)
        {
            DateTime lastDate = new DateTime(year, 12, 31);

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                lastDate,
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }

        public static IEnumerable<CalendarDay> GetCalendarMonth(int month, int year)
        {
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            var firstDay = GetFirstDateOfWeek(firstDayOfMonth);
            var lastDay = GetLastDateOfWeek(lastDayOfMonth);
            
            var lastDate = firstDay;
            while (lastDay >= lastDate)
            {
                yield return new CalendarDay(lastDate, lastDate.Month == month);

                lastDate = lastDate.AddDays(1);
            }
        }
    }
}