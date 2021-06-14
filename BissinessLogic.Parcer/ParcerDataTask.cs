using System;
using FluentScheduler;

namespace BissinessLogic.Parcer
{
    class ParcerDataTask : Registry
    {
        public ParcerDataTask()
        {
            this.Schedule(() => new ParcerJob())
                .ToRunOnceAt(DateTime.Now.AddSeconds(5))
                .AndEvery(1)
                .Hours();
        }
    }
}
