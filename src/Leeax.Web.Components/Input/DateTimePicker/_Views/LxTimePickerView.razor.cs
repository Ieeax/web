using Leeax.Web.Builders;
using System;

namespace Leeax.Web.Components.Input
{
    public partial class LxTimePickerView
    {
        public const string ClassName = "lx-timepickerview";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
            => builder.AddClassAttribute(ClassName, "lx-picker-view");

        private void NavigateToNow()
        {
            var today = DateTime.Now;
            ValueChanged.InvokeAsync(
                new DateTime(
                    Value.Year,
                    Value.Month,
                    Value.Day,
                    today.Hour,
                    today.Minute,
                    today.Second,
                    today.Millisecond));
        }
    }
}