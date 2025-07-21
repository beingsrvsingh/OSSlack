using Shared.Contracts.Interfaces;
using Shared.Infrastructure.Platform;
using System.Runtime.InteropServices;

namespace Shared.Infrastructure.Platform
{
    public static class PlatformServiceFactory
    {
        public static IPlatformService Create()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return new WindowsSecretManager();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return new MacKeychainManager();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return new LinuxSecretManager();

            throw new PlatformNotSupportedException("Unknown or unsupported OS platform.");
        }
    }
}
