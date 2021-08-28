using System;
using System.Threading.Tasks;
using Dadata;
using Dadata.Model;


namespace Core.GeoParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var token = "5a638547a2b338313dc6616ba2f8c96d7ee63d49";
            var secret = "28c0e1e69e748edd662a576cd94cbfd747600874";

            var api = new CleanClientAsync(token, secret);

            Address adr;
            try
            {
                adr = await api.Clean<Address>("Республика Беларусь Барановичи просп. Советский, 35");
                var pr = await api.Clean<Address>("Минск независимости 1");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void GetCoordinates(string addr)
        {

        }
    }
}
