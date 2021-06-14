﻿using System;
using FluentScheduler;

namespace BissinessLogic.Parser
{
    class ParserDataTask : Registry
    {
        public ParserDataTask()
        {
            this.Schedule(() => new ParserJob())
                .ToRunOnceAt(DateTime.Now.AddSeconds(5))
                .AndEvery(1)
                .Hours();
        }
    }
}