using System.Text.Json.Serialization;

namespace BusinessLogic.GeoParser.Models
{
    class Address
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("countryName")]
        public string CountryName { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("street")]
        public string Street { get; set; }
    }
}
