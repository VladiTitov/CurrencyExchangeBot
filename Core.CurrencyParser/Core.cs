using System;
using BusinessLogic.Parser;
using FluentScheduler;

namespace Core.CurrencyParser
{
    class Core
    {
        [Obsolete]
        static void Main()
        {
            JobManager.Initialize(new ParserDataTask());
            Console.ReadLine();
        }
    }
}
