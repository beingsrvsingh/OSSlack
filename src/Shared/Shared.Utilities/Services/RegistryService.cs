using System.Runtime.InteropServices;
using Microsoft.Win32;
using Utilities.Interfaces;

namespace Utilities.Services
{
    public class RegistryService : IRegistryService
    {
        private readonly static string registryPath = @"SOFTWARE\WOW6432Node\OSSlack";
        private readonly static string connectionStringName = "ConnectionString";
        private readonly static string tokenSeurityKeyName = "SecurityKey";

        private static RegistryKey? RegistryKey
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return Registry.CurrentUser.OpenSubKey(registryPath, true);
                }
                return null;
            }
        }

        public string? GetConnectionString()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return RegistryKey?.GetValue(connectionStringName)?.ToString();
            }
            return null;
        }

        public string ConnectionStringKeyName { get { return connectionStringName; } }

        public string TokenSeurityKeyName { get { return tokenSeurityKeyName; } }

        public string? GetSecurityKey()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return RegistryKey?.GetValue(tokenSeurityKeyName)?.ToString();
            }
            return null;
        }  

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

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                RegistryKey.SetValue(KeyName, Value);
            }
            else
            {
                throw new PlatformNotSupportedException("This method is only supported on Windows.");
            }
        }
    }
}
