using Leeax.Web.Builders;
using Leeax.Web.Components.Input;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Presentation
{
    public partial class LxPager
    {
        public const string ClassName = "lx-pager";
        public const string VariableBackgroundColor = ClassName + "-background";

        private readonly ZeroToOneBasedConverter _converter = new ZeroToOneBasedConverter();
        private int _total;

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, ClassNames.Unselectable)
                .AddSize(Size));
        }

        private IEnumerable<int> GetNavigation()
        {
            yield return 0;

            int start;
            int count;

            // Check if first page
            if (Page <= 1)
            {
                start = 1;
                count = 2;
            }
            else
            {
                start = Page - (Total > 3 && Page == (Total - 1) ? 2 : 1);
                count = 3;
            }

            var total = Total - 1;
            for (int i = 0; i < count; i++)
            {
                if (total > start + i)
                {
                    yield return start + i;
                }
            }

            if (total > 0)
            {
                // Add last page
                yield return total;
            }
        }

        #region Parameters

        /// <summary>
        /// Gets or sets the page index.
        /// The value is zero based.
        /// </summary>
        [Parameter]
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="Page"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<int> PageChanged { get; set; }

        /// <summary>
        /// Gets or sets the total count of pages.
        /// </summary>
        [Parameter]
        public int Total
        { 
            get => _total; 
            set
            {
                _total = value;

                if (Page >= _total 
                    && Page > 0) // Check if page is already 0 to prevent infinite loop
                {
                    // Adjust page to total
                    PageChanged.InvokeAsync(_total > 1 ? _total - 1 : 0);
                }
            }
        }

        /// <summary>
        /// Gets or sets the size. The default value is <see cref="ComponentSize.Small"/>.
        /// </summary>
        [Parameter]
        public ComponentSize Size { get; set; } = ComponentSize.Small;
        #endregion


        private class ZeroToOneBasedConverter : IConverter<int, SwitchOption>
        {
            public bool CanConvertBack(SwitchOption? value)
                => value != null;

            public SwitchOption Convert(int value)
                => new SwitchOption((value + 1).ToString(), value + 1);

            public int ConvertBack(SwitchOption? value)
                => value == null || value.Value == null ? 0 : (int)value.Value - 1;
        }
    }
}