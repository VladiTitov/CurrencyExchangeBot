using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogic.GeoParser.Models;

namespace BusinessLogic.GeoParser
{
    class GeoService
    {
        public HereResponse HereRequest(string url)
        {
            HereResponse response = new HereResponse();
            using var dataStream = GetDataStream(url).Result;
            using var dataReader = new StreamReader(dataStream);

            while (!dataReader.EndOfStream)
            {
                var objLine = dataReader.ReadLine();
                response = JsonSerializer.Deserialize<HereResponse>(objLine);
            }

            return response;
        }

        private async Task<Stream> GetDataStream(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync((url), HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
