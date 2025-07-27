
namespace Shared.Contracts.Interfaces;
public interface IKeyValueProvider
{
    string GetValue(string resourcePath, string keyName);
}
