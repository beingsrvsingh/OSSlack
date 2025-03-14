using Microsoft.Win32;
using Utilities.Interfaces;

namespace Utilities.Services
{
    public class RegistryService : IRegistryService
    {
        private readonly static string registryPath = @"SOFTWARE\WOW6432Node\OSSlack";
        private readonly static string connectionStringName = "ConnectionString";
        private readonly static string tokenSeurityKeyName = "SecurityKey";

        private static RegistryKey? RegistryKey => Registry.LocalMachine.OpenSubKey(registryPath, true);

        public string? GetConnectionString() => RegistryKey?.GetValue(connectionStringName)?.ToString();

        public string ConnectionStringKeyName { get { return connectionStringName; } }

        public string TokenSeurityKeyName { get { return tokenSeurityKeyName; } }

        public string? GetSecurityKey() => RegistryKey?.GetValue(tokenSeurityKeyName)?.ToString();

        public string GetRegistry(string key)
        {
            throw new NotImplementedException();
        }

        public void SetValue(String KeyName, String Value)
        {
            if(RegistryKey is null)
            {
                throw new ArgumentNullException(nameof(RegistryKey));
            }

            RegistryKey?.SetValue(KeyName, Value);
        }
    }
}
