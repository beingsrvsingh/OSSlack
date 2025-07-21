namespace Shared.Application.Interfaces.Platform
{
    public interface IRegistryService
    {
        string GetRegistry(string key);
        void SetValue(string KeyName, string Value);
    }
}
