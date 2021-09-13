using BusinessLogic.Parser.Parsers;
using FluentScheduler;

namespace BusinessLogic.Parser.Jobs
{
    class ParserCurrenciesJob : IJob
    {
        private readonly ParserContainer _containerService;

        public ParserCurrenciesJob() =>
            _containerService = new ParserContainer();

        public void Execute()
        {
            var container = _containerService.CreateContainer();
            container.GetInstance<ParserCurrencies>().Start();
        }
    }
}
