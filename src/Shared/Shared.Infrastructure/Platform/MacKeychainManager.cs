using System.Diagnostics;
using System.Text.RegularExpressions;
using Shared.Application.Interfaces.Logging;
using Shared.Contracts;
using Shared.Contracts.Interfaces;

namespace Shared.Infrastructure.Platform;

public class MacKeychainManager : IPlatform
{
    private readonly string KeyChainName;
    private readonly string KeyChainpassword;
    private readonly ILoggerService<MacKeychainManager> loggerService;

    private const int ProcessTimeoutSeconds = 10;

    public MacKeychainManager(ILoggerService<MacKeychainManager> loggerService, IKeyChainConfig keyChainConfig)
    {
        this.loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        if (keyChainConfig == null) throw new ArgumentNullException(nameof(keyChainConfig));

        this.KeyChainName = keyChainConfig.KeyChainName;
        this.KeyChainpassword = keyChainConfig.KeyChainPassword;

        if (string.IsNullOrWhiteSpace(this.KeyChainName) || string.IsNullOrWhiteSpace(this.KeyChainpassword))
            throw new InvalidOperationException("Keychain name or password is not set.");
    }

    public async Task<bool> CreateKeychainIfNotExistsAsync()
    {
        if (File.Exists(GetKeychainPath()))
            return true;

        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "/usr/bin/security",
                Arguments = $"create-keychain -p {KeyChainpassword} {KeyChainName}",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var proc = Process.Start(psi);
            if (proc == null)
                return false;

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(ProcessTimeoutSeconds));
            await proc.WaitForExitAsync(cts.Token);

            if (proc.ExitCode != 0)
            {
                var error = await proc.StandardError.ReadToEndAsync();
                loggerService.LogError("Failed to create keychain. Error: {Error}", error);
                return false;
            }

            return await UnlockKeychainAsync();
        }
        catch (Exception ex)
        {
            loggerService.LogError(ex, "Failed to add keychain.");
            return false;
        }
    }

    public async Task<IEnumerable<string>> GetAllCredentialKeysAsync()
    {
        var keys = new List<string>();

        if (!await UnlockKeychainAsync())
        {
            loggerService.LogWarning("Failed to unlock keychain.");
            return Enumerable.Empty<string>();
        }

        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "/usr/bin/security",
                Arguments = $"dump-keychain {GetKeychainPath()}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var proc = Process.Start(psi);
            if (proc == null)
                return Enumerable.Empty<string>();

            string output = await proc.StandardOutput.ReadToEndAsync();
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(ProcessTimeoutSeconds));
            await proc.WaitForExitAsync(cts.Token);

            if (proc.ExitCode != 0)
            {
                var error = await proc.StandardError.ReadToEndAsync();
                loggerService.LogWarning("Failed to dump keychain. Error: {Error}", error);
                return Enumerable.Empty<string>();
            }

            var matches = Regex.Matches(output, @"(?<=^ *""svce""<blob>="")[^""]+", RegexOptions.Multiline);
            keys.AddRange(matches.Select(m => m.Value));
            return keys.Distinct();
        }
        catch (Exception ex)
        {
            loggerService.LogError(ex, "Exception while retrieving key list.");
            return Enumerable.Empty<string>();
        }
    }

    public Task<string?> GetCredentialAsync(string keyName) => GetMacKeychainSecret(keyName);

    public async Task<bool> AddCredentialAsync(string keyName, string secret)
    {

        try
        {

            if (!await UnlockKeychainAsync())
            {
                loggerService.LogWarning("Failed to unlock keychain.");
                return false;
            }

            string keychainPath = GetKeychainPath();

            await CreateKeychainIfNotExistsAsync();

            var psi = new ProcessStartInfo
            {
                FileName = "/usr/bin/security",
                Arguments = $"add-generic-password -a {Environment.UserName} -s {keyName} -w {secret} -U {keychainPath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var proc = Process.Start(psi);
            if (proc == null)
                return false;

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(ProcessTimeoutSeconds));
            await proc.WaitForExitAsync(cts.Token);

            if (proc.ExitCode != 0)
            {
                string error = await proc.StandardError.ReadToEndAsync();
                loggerService.LogError("Failed to add credential '{Key}'. Error: {Error}", keyName, error);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            loggerService.LogError(ex, "Exception while adding credential '{Key}'.", keyName);
            return false;
        }
    }

    public async Task<bool> RemoveCredentialAsync(string keyName)
    {
        try
        {
            if (!await UnlockKeychainAsync())
            {
                loggerService.LogWarning("Failed to unlock keychain.");
                return false;
            }

            string keychainPath = GetKeychainPath();

            var psi = new ProcessStartInfo
            {
                FileName = "/usr/bin/security",
                Arguments = $"delete-generic-password -a {Environment.UserName} -s {keyName} {keychainPath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi);
            if (proc == null)
                return false;

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(ProcessTimeoutSeconds));
            await proc.WaitForExitAsync(cts.Token);

            if (proc.ExitCode != 0)
            {
                string error = await proc.StandardError.ReadToEndAsync();
                loggerService.LogWarning("Failed to remove credential '{Key}'. Error: {Error}", keyName, error);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            loggerService.LogError(ex, "Exception while removing credential '{Key}'.", keyName);
            return false;
        }
    }

    public async Task<bool> DeleteCustomKeychainAsync(string keychainName)
    {
        string keychainPath = GetKeychainPath();

        try
        {
            if (!File.Exists(keychainPath))
            {
                loggerService.LogWarning("Keychain file '{Path}' not found.", keychainPath);
                return false;
            }

            await Task.Run(() => File.Delete(keychainPath));
            loggerService.LogInfo("Deleted keychain at path: {Path}", keychainPath);
            return true;
        }
        catch (Exception ex)
        {
            loggerService.LogError(ex, "Error deleting keychain '{Path}'", keychainPath);
            return false;
        }
    }

    private async Task<string?> GetMacKeychainSecret(string serviceName)
    {
        try
        {
            if (!await UnlockKeychainAsync())
            {
                loggerService.LogWarning("Failed to unlock keychain.");
                return null;
            }

            string keychainPath = GetKeychainPath();

            var psi = new ProcessStartInfo
            {
                FileName = "/usr/bin/security",
                Arguments = $"find-generic-password -a {Environment.UserName} -s {serviceName} -w {keychainPath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var proc = Process.Start(psi);
            if (proc == null)
                return null;

            string output = await proc.StandardOutput.ReadToEndAsync();

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(ProcessTimeoutSeconds));
            await proc.WaitForExitAsync(cts.Token);

            if (proc.ExitCode != 0)
            {
                string error = await proc.StandardError.ReadToEndAsync();
                loggerService.LogWarning("Failed to retrieve credential for '{ServiceName}'. Error: {Error}", serviceName, error);
                return null;
            }

            return output.Trim();
        }
        catch (Exception ex)
        {
            loggerService.LogError(ex, "Exception while retrieving keychain secret for '{ServiceName}'.", serviceName);
            return null;
        }
    }

    private async Task<bool> UnlockKeychainAsync()
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "/usr/bin/security",
                Arguments = $"unlock-keychain -p {KeyChainpassword} {GetKeychainPath()}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var proc = Process.Start(psi);
            if (proc == null)
                return false;

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(ProcessTimeoutSeconds));
            await proc.WaitForExitAsync(cts.Token);

            if (proc.ExitCode != 0)
            {
                var error = await proc.StandardError.ReadToEndAsync();
                loggerService.LogWarning("Failed to unlock keychain. Error: {Error}", error);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            loggerService.LogError(ex, "Failed to unlock keychain.");
            return false;
        }
    }

    private string GetKeychainPath()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Library", "Keychains", KeyChainName);
    }
}
