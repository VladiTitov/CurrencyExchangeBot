using Serilog;

namespace BusinessLogic.Serilog
{
    public static class LoggingService
    {
        static LoggingService()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/currencyBot_.log", rollingInterval: RollingInterval.Hour)
                .CreateLogger();
        }

        public static void AddEventToLog(string message) => Log.Information(message);
    }
}
