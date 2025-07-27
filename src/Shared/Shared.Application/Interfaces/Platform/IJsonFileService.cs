
namespace Shared.Application.Interfaces.Platform
{
    public interface IJsonFileService
    {
        Task<T?> ReadAsync<T>(string filePath) where T : class;
        Task<string?> ReadAsync(string filePath);
        Task<bool> WriteAsync<T>(string filePath, T data) where T : class;
    }
}