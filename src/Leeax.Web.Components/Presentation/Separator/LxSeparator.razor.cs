using Leeax.Web.Builders;
using System;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Presentation
{
    public partial class LxSeparator
    {
        public const string ClassName = "lx-separator";
        public const string VariableColor = ClassName + "-color";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .Add(ClassName)
                .Add("my-2 mx-1", Orientation == Orientation.Horizontal)
                .Add("my-1 mx-2", Orientation == Orientation.Vertical)
                .Add(Orientation switch
                {
                    Orientation.Vertical => "vertical",
                    Orientation.Horizontal => "horizontal",
                    _ => throw new NotImplementedException()
                }));
        }

        /// <summary>
        /// Gets or sets the orientation. The default value is <see cref="Orientation.Vertical"/>.
        /// </summary>
        [Parameter]
        public Orientation Orientation { get; set; } = Orientation.Vertical;
    }
}