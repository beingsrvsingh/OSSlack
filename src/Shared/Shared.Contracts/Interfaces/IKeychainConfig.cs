
namespace Shared.Contracts;

public interface IKeyChainConfig
{  
    public const string seperatorPrefix = "_";
    string GetStaticPath(string fileName);
    string EnvPrefix { get; }
    string KeyChainName { get; }
    string KeyChainPassword { get; }
    string AddEnvPrefix(string keyName, string seperator = seperatorPrefix);

}