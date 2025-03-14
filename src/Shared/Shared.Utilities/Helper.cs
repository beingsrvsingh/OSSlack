using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Shared.Utilities
{
    public class Helper
    {
        public static IConfigurationRoot LoadAppSettings()
        {
            return new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                                .Build();
        }

        public static readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
