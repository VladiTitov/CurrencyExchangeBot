using System;
using BusinessLogic.BotConnection;
using BissinessLogic.Parser;
using FluentScheduler;

namespace Core.CurrencyExchangeBot
{
    class Core
    {
        [Obsolete]
        static void Main()
        {
            var connection = new Connection("1401702551:AAHrr7hEYPKXLXdLgvI6zWYsxgzA-Ra24ms"); 
            //JobManager.Initialize(new ParserDataTask());
            connection.Start();

            Console.ReadLine();
        }
    }
}
