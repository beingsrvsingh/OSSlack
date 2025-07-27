using Shared.Application.Interfaces.Logging;
using Shared.Application.Interfaces.Platform;
using Shared.Utilities;

namespace Identity.Infrastructure.Services
{
    public class JsonFileService : IJsonFileService
    {
        private readonly ILoggerService<JsonFileService> _logger;
        public JsonFileService(ILoggerService<JsonFileService> logger)
        {
            this._logger = logger;

        }
        public async Task<T?> ReadAsync<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath))
            {
                _logger.LogWarning("File not found at path: {FilePath}", filePath);
                return null;
            }

            try
            {
                using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                return JsonSerializerWrapper.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to read or deserialize JSON from {FilePath}", filePath);
                return null;
            }
        }

        public async Task<string?> ReadAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                _logger.LogWarning("File not found at path: {FilePath}", filePath);
                return null;
            }

            try
            {
                using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var reader = new StreamReader(stream);

                var json = await reader.ReadToEndAsync();

                if (!string.IsNullOrWhiteSpace(json))
                    return json;

                _logger.LogWarning("File at {FilePath} is empty.", filePath);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to read or deserialize JSON from {FilePath}", filePath);
                return null;
            }
        }

        public async Task<bool> WriteAsync<T>(string filePath, T data) where T : class
        {
            try
            {
                var json = JsonSerializerWrapper.Serialize(data);

                using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                using var writer = new StreamWriter(stream);
                await writer.WriteAsync(json);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to write JSON to {FilePath}", filePath);
                return false;
            }
        }
    }
}