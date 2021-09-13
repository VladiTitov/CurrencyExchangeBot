using System;
using BusinessLogic.Parser.Jobs;
using FluentScheduler;

namespace BusinessLogic.Parser
{
    public class ParserDataTask : Registry
    {
        public ParserDataTask()
        {
            this.Schedule(() => new ParserCitiesJob())
                .ToRunOnceAt(DateTime.Now.AddSeconds(30))
                .AndEvery(1)
                .Days();

            this.Schedule(() => new ParserCurrenciesJob())
                .ToRunOnceAt(DateTime.Now.AddMinutes(2))
                .AndEvery(1)
                .Days();

            this.Schedule(() => new ParserJob())
                .ToRunOnceAt(DateTime.Now.AddMinutes(5))
                .AndEvery(20)
                .Minutes();

        }
    }
}
