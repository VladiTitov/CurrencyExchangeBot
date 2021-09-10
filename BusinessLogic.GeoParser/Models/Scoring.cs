using System.Text.Json.Serialization;

namespace BusinessLogic.GeoParser.Models
{
    class Scoring
    {
        [JsonPropertyName("queryScore")]
        public double QueryScore { get; set; }

        [JsonPropertyName("fieldScore")]
        public FieldScore FieldScore { get; set; }
    }
}
