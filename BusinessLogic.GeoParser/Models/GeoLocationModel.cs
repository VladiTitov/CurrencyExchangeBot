namespace BusinessLogic.GeoParser.Models
{
    public class GeoLocationModel
    {
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public GeoLocationModel(string address, float latitude, float longitude)
        {
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
