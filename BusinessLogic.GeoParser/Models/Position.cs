using System.Text.Json.Serialization;

namespace BusinessLogic.GeoParser.Models
{
    class Position
    {
        [JsonPropertyName("lat")]
        public float Lat { get; set; }

        [JsonPropertyName("lng")]
        public float Lng { get; set; }
    }
}
