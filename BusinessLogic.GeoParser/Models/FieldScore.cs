using System.Text.Json.Serialization;

namespace BusinessLogic.GeoParser.Models
{
    class FieldScore
    {
        [JsonPropertyName("country")]
        public double Country { get; set; }

        [JsonPropertyName("city")]
        public double City { get; set; }

        [JsonPropertyName("streets")]
        public double[] Streets { get; set; }
    }
}
