using System.Text.Json.Serialization;

namespace BusinessLogic.GeoParser.Models
{
    class Item
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("resultType")]
        public string ResultType { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("position")]
        public Position Position { get; set; }

        [JsonPropertyName("mapView")]
        public MapView MapView { get; set; }

        [JsonPropertyName("scoring")]
        public Scoring Scoring { get; set; }
    }
}
