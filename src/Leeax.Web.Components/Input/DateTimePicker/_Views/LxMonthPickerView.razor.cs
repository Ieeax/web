using Leeax.Web.Builders;
using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Input
{
    public partial class LxMonthPickerView
    {
        public const string ClassName = "lx-monthpickerview";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
            => builder.AddClassAttribute(ClassName, "lx-picker-view");

        private bool CanSubtractYear()
            => ShownValue.Year > 1;

        private void NavigateBackward()
        {
            if (CanSubtractYear())
            {
                ShownValueChanged.InvokeAsync(ShownValue.AddYears(-1));
            }
        }

        private void NavigateForward()
            => ShownValueChanged.InvokeAsync(ShownValue.AddYears(1));

        private void OnMonthClicked(int month)
        {
            ShownValueChanged.InvokeAsync(new DateTime(
                ShownValue.Year,
                month,
                ShownValue.Day,
                ShownValue.Hour,
                ShownValue.Minute,
                ShownValue.Second,
                ShownValue.Millisecond));
        }

        private string GetMonthName(DateTime value, bool shortForm = false)
            => value.ToString(shortForm ? "MMM" : "MMMM", FormatProvider);

        private string Heading => ShownValue.Year.ToString();

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