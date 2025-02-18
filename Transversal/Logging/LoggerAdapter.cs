using Microsoft.Extensions.Logging;
// using WatchDog;

namespace Dls.Erp.Transversal.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }
        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);            
            // WatchLogger.LogError(message);
        }

        public void LogInformacion(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
            // WatchLogger.Log(message);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
            // WatchLogger.LogWarning(message);
        }
    }
}
