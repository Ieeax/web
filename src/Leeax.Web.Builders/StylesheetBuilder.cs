namespace Leeax.Web.Builders
{
    public class StylesheetBuilder
    {
        private string? _value;

        public void AddString(string? value)
        {
            _value += value;
        }

        public string? Build()
            => _value;

        public override string? ToString()
            => Build();
    }
}