namespace Utilities.Interfaces
{
    public interface IRegistryService
    {
        string? GetSecurityKey();

        string GetRegistry(string key);

        string? GetConnectionString();

        void SetValue(string KeyName, string Value);

        string ConnectionStringKeyName { get; }

        string TokenSeurityKeyName { get; }
    }
}
