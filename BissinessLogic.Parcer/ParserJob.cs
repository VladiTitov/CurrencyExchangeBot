using FluentScheduler;

namespace BusinessLogic.Parser
{
    class ParserJob : IJob
    {
        private readonly ParserContainer _containerService;

        public ParserJob() => 
            _containerService = new ParserContainer();

        public void Execute()
        {
            var container = _containerService.CreateContainer();
            container.GetInstance<Parser>().Start();
        }
    }
}
