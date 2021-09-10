using BusinessLogic.GeoParser.Models;

namespace BusinessLogic.GeoParser
{
    public class GeoHandler
    {
        private readonly string _token;
        private readonly GeoService _service;

        string resource = @"https://discover.search.hereapi.com/v1";

        public GeoHandler(string token)
        {
            _token = token;
            _service = new GeoService();
        }

        public GeoLocationModel SearchByAddress(string address)
        {
            var urlRequest = CreateRequest("/geocode?q", address);
            var response = _service.HereRequest(urlRequest);
            return new GeoLocationModel(
                "",
                response.Items[0].Position.Lat,
                response.Items[0].Position.Lng
            );
        }

        public GeoLocationModel SearchByCoordinates(string latitude, string longitude)
        {
            var urlRequest = CreateRequest("/revgeocode?at", $"{latitude},{longitude}");
            var response = _service.HereRequest(urlRequest);
            return new GeoLocationModel(
                "", 
                response.Items[0].Position.Lat, 
                response.Items[0].Position.Lng
                );
        }

        private string CreateRequest(string typeRequest, string source) => 
            $"{resource}{typeRequest}={source}&apiKey={_token}";
    }
}
