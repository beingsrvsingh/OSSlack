namespace Shared.Application.Interfaces.Logging
{
    public interface ILoggerService<T>
    {
        void LogError(Exception ex, string errorMessage, params object[] args);

        void LogError(Exception ex);

        void LogError(string errorMessage);

        void LogError(string errorMessage, params object[] args);

        void LogException(Exception ex);

        void LogInfo(string infoMessage);

        void LogInfo(string infoMessage, params object[] args);

        void LogWarning(string errorMessage, params object[] args);

        void LogCritical(Exception ex, string errorMessage, params object[] args);
    }
}
