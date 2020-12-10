using Leeax.Web.Builders;
using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Input
{
    public partial class LxDatePickerView
    {
        public const string ClassName = "lx-datepickerview";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
            => builder.AddClassAttribute(ClassName, "lx-picker-view");

        private string? GetCalendarItemStyle(CalendarDay day)
        {
            return ClassBuilder.Create()
                .Add(ClassNames.ThemePrimaryForegroundColor, day.Date.Date == Value.Date)
                .Add(ClassNames.Selected + " " + ClassNames.FontWeightBold, day.Date.Date == Value.Date)
                .Build();
        }

        private bool CanSubtractMonth()
            => ShownValue.Year > 1 || ShownValue.Month > 1;

        private void NavigateToToday()
        {
            var today = DateTime.Now;
            ShownValueChanged.InvokeAsync(
                new DateTime(
                    today.Year,
                    today.Month,
                    today.Day,
                    ShownValue.Hour,
                    ShownValue.Minute,
                    ShownValue.Second,
                    ShownValue.Millisecond));
        }

        private void NavigateBackward()
        {
            if (CanSubtractMonth())
            {
                ShownValueChanged.InvokeAsync(ShownValue.AddMonths(-1));
            }
        }

        private void NavigateForward()
            => ShownValueChanged.InvokeAsync(ShownValue.AddMonths(1));

        private void OnDateClicked(CalendarDay day)
        {
            ValueChanged.InvokeAsync(
                new DateTime(
                    day.Year,
                    day.Month,
                    day.Day,
                    Value.Hour,
                    Value.Minute,
                    Value.Second,
                    Value.Millisecond));
        }

        private string GetMonthName(DateTime value, IFormatProvider? formatProvider, bool shortForm = false)
            => value.ToString(shortForm ? "MMM" : "MMMM", formatProvider);

        private string Heading => GetMonthName(ShownValue, FormatProvider) + " " + ShownValue.Year;

        /// <summary>
        /// Gets or sets the shown value. This value can differ from the actual value.
        /// </summary>
        [Parameter]
        public DateTime ShownValue { get; set; }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="ShownValue"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<DateTime> ShownValueChanged { get; set; }

        /// <summary>
        /// Gets or set the <see cref="IFormatProvider"/> for formatting the month.
        /// </summary>
        [Parameter]
        public IFormatProvider? FormatProvider { get; set; } = CultureInfo.InvariantCulture;
    }
}