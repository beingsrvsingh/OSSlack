using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32;
using Shared.Application.Interfaces.Platform;

namespace Utilities.Services
{
    [SupportedOSPlatform("windows")]
    public class RegistryService : IRegistryService
    {
        private readonly static string registryPath = @"SOFTWARE\WOW6432Node\OSSlack";

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

        public string GetRegistry(string key)
        {
            return RegistryKey?.GetValue(key)?.ToString() ?? throw new KeyNotFoundException($"Key '{key}' not found in registry.");
        }

        public void SetValue(String KeyName, String Value)
        {
            if (RegistryKey is null)
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
