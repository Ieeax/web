namespace Leeax.Web.Components
{
    public readonly struct Binding
    {
        public Binding(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                Path = null;
                BoundToSelf = false;
                HasValue = false;
                return;
            }

            BoundToSelf = path == ".";
            Path = BoundToSelf ? null : path;
            HasValue = true;
        }

        public static bool TryParse(string? value, out Binding binding)
        {
            if (value != null
                && value.Length > 2
                && value[0] == '{'
                && value[^1] == '}')
            {
                binding = new Binding(value[1..^1]);
                return true;
            }

            binding = Binding.Empty;
            return false;
        }

        public override string? ToString()
            => HasValue 
                ? BoundToSelf 
                    ? "{.}" 
                    : "{" + Path + "}" 
                : null;

        public string? Path { get; }

        public bool BoundToSelf { get; }

        public bool HasValue { get; }

        public static Binding Self { get; } = new Binding(".");

        public static Binding Empty { get; } = new Binding();
    }
}