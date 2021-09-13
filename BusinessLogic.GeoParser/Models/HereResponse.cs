using System.Text.Json.Serialization;

namespace BusinessLogic.GeoParser.Models
{
    class HereResponse
    {
        [JsonPropertyName("items")]
        public Item[] Items { get; set; }
    }
}
