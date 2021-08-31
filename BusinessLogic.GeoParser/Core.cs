using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusinessLogic.GeoParser
{
    public class Core
    {
        string resource = @"https://discover.search.hereapi.com/v1";

        string token = "-M5H8R8duUif915aAMT8l9wWxOeQoMb_UgXFo4hB8WI";

        public Core()
        {


        }

        private string CreateRequest(string typeRequest, string source) => $"{resource}{typeRequest}={source}4&apiKey={token}";

        private async Task<Stream> GetDataStream(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync((url), HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

       
        public void SearchByAdress(string address)
        {
            string key = "/geocode?q";

            var urlRequest = CreateRequest(key, address);

            using var data_stream = GetDataStream(urlRequest).Result;
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var objLine = data_reader.ReadLine();
            }
        }

        public void SearchByCoordinates(string latitude, string longitude)
        {
            string key = "/revgeocode?at"; 

            var urlRequest = CreateRequest(key, $"{latitude},{longitude}");

            using var data_stream = GetDataStream(urlRequest).Result;
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var objLine = data_reader.ReadLine();
            }
        }
    }
}
