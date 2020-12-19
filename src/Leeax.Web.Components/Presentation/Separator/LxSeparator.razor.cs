using Leeax.Web.Builders;

namespace Leeax.Web.Components.Presentation
{
    public partial class LxSeparator
    {
        public const string ClassName = "lx-separator";
        public const string VariableColor = ClassName + "-color";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName);
        }
    }
}