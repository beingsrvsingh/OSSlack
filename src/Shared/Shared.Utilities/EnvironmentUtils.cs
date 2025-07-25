
namespace Shared.Utilities
{
    public static class EnvironmentUtils
    {
        public static string GetEnv(string environmentName)
        {
            if (string.IsNullOrEmpty(environmentName))
                return "LOCALHOST";

            return environmentName.ToLowerInvariant() switch
            {
                "development" => "DEV",
                "testing" => "TEST",
                "staging" => "STAGING",
                "production" => "PROD",
                _ => "PROD"
            };
        }

        public static string AddEnvironmentPrefix(string keyName, string prefix) => $"{prefix}{keyName}";

    }
}
