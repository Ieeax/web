namespace Leeax.Web.Components.Input
{
    public partial class LxInputNumber : LxInput<double>
    {
        public LxInputNumber()
            : base("number")
        {
            Converter = new DoubleStringConverter(null, Culture);
        }
    }
}