using System.Runtime.InteropServices;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Shared.Application.Interfaces.Logging;
using Shared.Contracts.Interfaces;
using Shared.Utilities;

namespace Shared.Infrastructure.Platform;

public class WindowsSecretManager : IPlatform
{
    private readonly ILoggerService<WindowsSecretManager> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _envPrefix;

    public WindowsSecretManager(ILoggerService<WindowsSecretManager> logger, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        _envPrefix = EnvironmentUtils.GetEnv(_webHostEnvironment.EnvironmentName);
    }

    public Task<IEnumerable<string>> GetAllCredentialKeysAsync()
    {
        var keys = new List<string>();

        try
        {
            if (!CredEnumerate(null, 0, out int count, out var pCredentials))
            {
                int err = Marshal.GetLastWin32Error();
                _logger.LogWarning("CredEnumerate failed with error code {ErrorCode}", err);
                return Task.FromResult<IEnumerable<string>>(keys);
            }

            for (int i = 0; i < count; i++)
            {
                var credPtr = Marshal.ReadIntPtr(pCredentials, i * IntPtr.Size);
                if (credPtr == IntPtr.Zero) continue;

                var cred = Marshal.PtrToStructure<NativeCredential>(credPtr);
                if (!string.IsNullOrWhiteSpace(cred.TargetName) && cred.TargetName.StartsWith(_envPrefix))
                {
                    string key = cred.TargetName.Substring(_envPrefix.Length);
                    keys.Add(key);
                }
            }

            CredFree(pCredentials);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to enumerate Windows credentials.");
        }

        return Task.FromResult<IEnumerable<string>>(keys);
    }

    public async Task<string?> GetCredentialAsync(string keyName)
    {
        var fullKey = EnvironmentUtils.AddEnvironmentPrefix(keyName, _envPrefix);
        return await GetWindowsCredentialAsync(fullKey);
    }

    public Task<bool> AddCredentialAsync(string keyName, string secret)
    {
        var fullKey = EnvironmentUtils.AddEnvironmentPrefix(keyName, _envPrefix);
        var bytes = Encoding.Unicode.GetBytes(secret);

        var credential = new CREDENTIAL
        {
            Type = CRED_TYPE_GENERIC,
            TargetName = fullKey,
            CredentialBlobSize = (uint)bytes.Length,
            CredentialBlob = Marshal.AllocCoTaskMem(bytes.Length),
            Persist = (uint)CredentialPersistence.Enterprise,
            AttributeCount = 0,
            Attributes = IntPtr.Zero,
            Comment = "Stored via WindowsSecretManager",
            TargetAlias = null,
            UserName = Environment.UserName
        };

        try
        {
            Marshal.Copy(bytes, 0, credential.CredentialBlob, bytes.Length);

            if (!CredWrite(ref credential, 0))
            {
                int err = Marshal.GetLastWin32Error();
                _logger.LogError("CredWrite failed for key '{Key}' with error code {ErrorCode}", fullKey, err);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add credential '{KeyName}'", fullKey);
            return Task.FromResult(false);
        }
        finally
        {
            Marshal.FreeCoTaskMem(credential.CredentialBlob);
        }
    }

    public Task<bool> RemoveCredentialAsync(string keyName)
    {
        var fullKey = EnvironmentUtils.AddEnvironmentPrefix(keyName, _envPrefix);
        bool success = CredDelete(fullKey, CRED_TYPE_GENERIC, 0);

        if (!success)
        {
            int err = Marshal.GetLastWin32Error();
            _logger.LogWarning("Failed to delete credential '{Key}' with error code {ErrorCode}", fullKey, err);
        }

        return Task.FromResult(success);
    }

    private static Task<string?> GetWindowsCredentialAsync(string fullKey)
    {
        if (!CredRead(fullKey, CRED_TYPE_GENERIC, 0, out var credPtr))
            return Task.FromResult<string?>(null);

        try
        {
            var cred = Marshal.PtrToStructure<CREDENTIAL>(credPtr);
            if (cred.CredentialBlob == IntPtr.Zero || cred.CredentialBlobSize == 0)
                return Task.FromResult<string?>(null);

            string? secret = Marshal.PtrToStringUni(cred.CredentialBlob, (int)cred.CredentialBlobSize / 2);
            return Task.FromResult<string?>(secret);
        }
        finally
        {
            CredFree(credPtr);
        }
    }

    #region P/Invoke and Structures

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool CredWrite([In] ref CREDENTIAL userCredential, [In] uint flags);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool CredRead(string target, uint type, int reservedFlag, out IntPtr credentialPtr);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern void CredFree(IntPtr buffer);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool CredDelete(string target, uint type, uint flags);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool CredEnumerate(string? filter, uint flags, out int count, out IntPtr pCredentials);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct CREDENTIAL
    {
        public uint Flags;
        public uint Type;
        public string TargetName;
        public string? Comment;
        public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        public uint CredentialBlobSize;
        public IntPtr CredentialBlob;
        public uint Persist;
        public uint AttributeCount;
        public IntPtr Attributes;
        public string? TargetAlias;
        public string? UserName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct NativeCredential
    {
        public uint Flags;
        public uint Type;
        public string? TargetName;
        public string? Comment;
        public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        public uint CredentialBlobSize;
        public IntPtr CredentialBlob;
        public uint Persist;
        public uint AttributeCount;
        public IntPtr Attributes;
        public string? TargetAlias;
        public string? UserName;
    }

    private const uint CRED_TYPE_GENERIC = 1;

    private enum CredentialPersistence : uint
    {
        Session = 1,
        LocalMachine,
        Enterprise
    }

    #endregion
}
