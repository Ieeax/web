using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Input
{
    public partial class TemplateCalendarMonth
    {
        [Parameter]
        public int Month { get; set; }

        [Parameter]
        public int Year { get; set; }

        [Parameter]
        public RenderFragment<CalendarDay>? EntryTemplate { get; set; }

        [Parameter]
        public IFormatProvider? Culture { get; set; } = CultureInfo.InvariantCulture;
    }
}