﻿using System;
using BusinessLogic.Parser;
using FluentScheduler;

namespace Core.CurrencyParser
{
    class Core
    {
        [Obsolete]
        static void Main()
        {
            JobManager.Initialize(new ParserDataTask(delay: 10, interval: 15));
            Console.ReadLine();
        }
    }
}
