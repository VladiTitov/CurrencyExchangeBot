using System;
using BisinessLogic.BotConnection;

namespace Core.CurrencyExchangeBot
{
    class Core
    {
        static void Main()
        {
            var connection = new Connection("1401702551:AAHrr7hEYPKXLXdLgvI6zWYsxgzA-Ra24ms");
            connection.Start();

            Console.ReadLine();
        }
    }
}
