using System.Text.Json.Serialization;

namespace Leeax.Web.Components.Theme
{
    public class ThemeState
    {
        public ThemeState()
        {
        }

        public ThemeState(string? name)
        {
            Name = name;
        }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}