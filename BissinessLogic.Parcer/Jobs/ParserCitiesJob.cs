using BusinessLogic.Parser.Parsers;
using FluentScheduler;

namespace BusinessLogic.Parser.Jobs
{
    class ParserCitiesJob : IJob
    {
        private readonly ParserContainer _containerService;

        public ParserCitiesJob() =>
            _containerService = new ParserContainer();

        public void Execute()
        {
            var container = _containerService.CreateContainer();
            container.GetInstance<ParserCities>().Start();
        }
    }
}
