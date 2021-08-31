using System;
using BusinessLogic.Parser.Jobs;
using FluentScheduler;

namespace BusinessLogic.Parser
{
    public class ParserDataTask : Registry
    {
        public ParserDataTask(int delay, int interval)
        {
            this.Schedule(() => new ParserCitiesJob())
                .ToRunOnceAt(DateTime.Now.AddSeconds(1))
                .AndEvery(1)
                .Days();

            this.Schedule(() => new ParserCurrenciesJob())
                .ToRunOnceAt(DateTime.Now.AddSeconds(20))
                .AndEvery(1)
                .Days();

            this.Schedule(() => new ParserJob())
                .ToRunOnceAt(DateTime.Now.AddSeconds(40))
                .AndEvery(1)
                .Hours();

        }
    }
}
