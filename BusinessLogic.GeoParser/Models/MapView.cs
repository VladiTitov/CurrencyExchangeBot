using System.Text.Json.Serialization;

namespace BusinessLogic.GeoParser.Models
{
    class MapView
    {
        [JsonPropertyName("west")]
        public double West { get; set; }

        [JsonPropertyName("south")]
        public double South { get; set; }

        [JsonPropertyName("east")]
        public double East { get; set; }

        [JsonPropertyName("north")]
        public double North { get; set; }
    }
}
