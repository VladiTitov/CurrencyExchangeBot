using System;
using FluentScheduler;

namespace BusinessLogic.Parser
{
    public class ParserDataTask : Registry
    {
        public ParserDataTask(int delay, int interval)
        {
            this.Schedule(() => new ParserJob())
                .ToRunOnceAt(DateTime.Now.AddSeconds(delay))
                .AndEvery(interval)
                .Hours();
        }
    }
}
