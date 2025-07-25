using NLog.Web;
using NLog;
using Shared.Application.Interfaces.Logging;

namespace Shared.Infrastructure.Services
{
    public class LoggerService<T> : ILoggerService<T>
    {
        // Create nlog.config (lowercase all) file in the root of your project
        protected NLog.Logger logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config")!.GetCurrentClassLogger();

        public void LogError(string errorMessage)
        {
            logger.Error(errorMessage);
        }

        public void LogError(string errorMessage, params object[] args)
        {
            logger.Error(errorMessage, args);
        }

        public void LogError(Exception ex, string errorMessage, params object[] args)
        {
            logger.Error(ex, errorMessage, args);
        }

        public void LogError(Exception ex)
        {
            logger.Error(ex);
        }

        public void LogException(Exception ex)
        {
            logger.Error(ex);
        }

        public void LogInfo(string infoMessage)
        {
            logger.Info(infoMessage);
        }

        public void LogInfo(string infoMessage, params object[] args)
        {
            logger.Info(infoMessage, args);
        }

        public void LogWarning(string errorMessage, params object[] args)
        {
            logger.Warn(errorMessage, args);
        }

        public void LogCritical(Exception ex, string errorMessage, params object[] args)
        {
            logger.Log(NLog.LogLevel.Fatal, ex, errorMessage, args);
        }
    }
}
