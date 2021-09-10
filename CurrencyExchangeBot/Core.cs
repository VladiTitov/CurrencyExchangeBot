using System;
using BusinessLogic.BotConnection;

namespace Core.CurrencyExchangeBot
{
    static class Core
    {
        [Obsolete("This property is obsolete")]
        static void Main()
        {
            var connection = new Connection();
            connection.Connect();

            Console.ReadLine();
        }
    }
}
