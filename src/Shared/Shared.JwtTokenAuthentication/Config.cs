using Microsoft.Extensions.Configuration;

namespace JwtToken
{
    internal static class Config
    {
        private static IConfiguration configuration;

        static Config()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.AppContext.BaseDirectory)
                .AddJsonFile($"appSettings.{env}.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();
        }

        public static string Get(string name)
        {
            
            string appSettings = configuration[name];
            return appSettings;
        }

        public static IConfigurationSection GetSection(string name)
        {
            return configuration.GetSection(name);
        }
    }
}
