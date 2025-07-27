using Microsoft.Extensions.Configuration;
using Shared.Utilities.Interfaces;

namespace Shared.Utilities
{
    public class Configuration : IConfig
    {
        public static string GetEnvironmentVariable => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? IConfig.defaultEnvironmentVairable;

        public static IConfigurationRoot LoadAppSettings(string fileName = IConfig.defaultFileName)
        {
            return new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile($"{fileName}.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"{fileName}.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                                .Build();
        }

        public static T GetValue<T>(string sectionName, string keyName, string fileName = IConfig.defaultFileName)
        {
            var config = LoadAppSettings(fileName);
            var section = config.GetSection(sectionName);

            if (!section.Exists())
                throw new InvalidOperationException($"Configuration section '{sectionName}' does not exist.");

            T? configValue = section.GetValue<T>(keyName);

            if (configValue == null)
                throw new InvalidOperationException($"Key '{keyName}' in section '{sectionName}' is not configured.");

            return configValue;
        }

        public static string GetPath(string fileName = IConfig.defaultFileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}.json");
            return path;
        }
    }
}
