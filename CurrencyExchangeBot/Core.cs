using System;
using BisinessLogic.BotConnection;
using BissinessLogic.Parser;
using FluentScheduler;

namespace Core.CurrencyExchangeBot
{
    class Core
    {
        static void Main()
        {
            var connection = new Connection("1401702551:AAHrr7hEYPKXLXdLgvI6zWYsxgzA-Ra24ms");
            JobManager.Initialize(new ParserDataTask());
            connection.Start();

            Console.ReadLine();
        }
    }
}
