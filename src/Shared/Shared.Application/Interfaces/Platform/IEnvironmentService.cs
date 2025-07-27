

namespace SecretManagement.Application.Services.Interfaces;

public interface IEnvironmentService
{
    string GetStaticPath(string fileName);
    string? GetVariable(string key);
    T? GetJsonObject<T>(string key) where T : class;
    bool IsSet(string key);
    Task<bool> SetVariable(string key, string value);
    Task<bool> RemoveVariable(string key);
}