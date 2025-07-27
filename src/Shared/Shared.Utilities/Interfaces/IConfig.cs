using Microsoft.Extensions.Configuration;

namespace Shared.Utilities.Interfaces
{
    public interface IConfig
    {
        public const string defaultEnvironmentVairable = "Development";
        public const string defaultFileName = "appsettings";
        abstract static IConfigurationRoot LoadAppSettings(string fileName = defaultFileName);
        abstract static T GetValue<T>(string sectionName, string keyName, string fileName = IConfig.defaultFileName);
        abstract static string GetPath(string fileName = IConfig.defaultFileName);
        abstract static string GetEnvironmentVariable { get; }
    }
}