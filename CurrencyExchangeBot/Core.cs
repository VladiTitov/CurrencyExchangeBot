using System;
using BusinessLogic.BotConnection;

namespace Core.CurrencyExchangeBot
{
    static class Core
    {
        static void Main()
        {
            var connection = new Connection("1401702551:AAHrr7hEYPKXLXdLgvI6zWYsxgzA-Ra24ms");
            connection.Connect();

            Console.ReadLine();
        }
    }
}
