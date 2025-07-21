
using System.Runtime.InteropServices;
using System.Text;
using Shared.Contracts.Interfaces;


namespace Shared.Infrastructure.Platform;

public class WindowsSecretManager : IPlatformService
{

    public IEnumerable<string> GetAllCredentialKeys()
    {
        var keys = new List<string>();

        if (!CredEnumerate(null, 0, out int count, out var pCredentials))
            return keys;

        for (int i = 0; i < count; i++)
        {
            var credPtr = Marshal.ReadIntPtr(pCredentials, i * IntPtr.Size);
            var cred = Marshal.PtrToStructure<NativeCredential>(credPtr);
            keys.Add(cred.TargetName);
        }

        CredFree(pCredentials);
        return keys;
    }

    public string? GetCredential(string keyName) => GetWindowsCredential(keyName);

    public void AddCredential(string keyName, string secret)
    {
        var byteArray = Encoding.Unicode.GetBytes(secret);

        var credential = new CREDENTIAL
        {
            Type = CRED_TYPE_GENERIC,
            TargetName = keyName,
            CredentialBlobSize = (uint)byteArray.Length,
            Persist = (uint)CredentialPersistence.Enterprise,
            CredentialBlob = Marshal.AllocCoTaskMem(byteArray.Length),
            AttributeCount = 0,
            Attributes = IntPtr.Zero,
            TargetAlias = null,
            Comment = "Stored via .NET app",
            UserName = Environment.UserName
        };

        Marshal.Copy(byteArray, 0, credential.CredentialBlob, byteArray.Length);

        bool result = CredWrite(ref credential, 0);

        Marshal.FreeCoTaskMem(credential.CredentialBlob);

        if (!result)
        {
            int err = Marshal.GetLastWin32Error();
            throw new Exception($"CredWrite failed with error code {err}");
        }
    }

    public void RemoveCredential(string keyName)
    {
        CredDelete(keyName, 1, 0); // 1 = CRED_TYPE_GENERIC
    }

    #region P/Invoke and Supporting Code

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool CredWrite([In] ref CREDENTIAL userCredential, [In] uint flags);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool CredRead(string target, uint type, int reservedFlag, out IntPtr credentialPtr);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern void CredFree(IntPtr buffer);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool CredDelete(string target, uint type, uint flags);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool CredEnumerate(string filter, uint flags, out int count, out IntPtr pCredentials);

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

    private const uint CRED_TYPE_GENERIC = 1;

    private enum CredentialPersistence : uint
    {
        Session = 1,
        LocalMachine,
        Enterprise
    }

    private static string? GetWindowsCredential(string targetName)
    {
        if (!CredRead(targetName, 1, 0, out var credPtr))
            return null;

        try
        {
            var cred = (CREDENTIAL)Marshal.PtrToStructure(credPtr, typeof(CREDENTIAL))!;
            var secret = Marshal.PtrToStringUni(cred.CredentialBlob, (int)cred.CredentialBlobSize / 2);
            return secret;
        }
        finally
        {
            CredFree(credPtr);
        }
    }

     [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct NativeCredential
    {
        public uint Flags;
        public uint Type;
        public string TargetName;
        public string Comment;
        public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        public uint CredentialBlobSize;
        public IntPtr CredentialBlob;
        public uint Persist;
        public uint AttributeCount;
        public IntPtr Attributes;
        public string TargetAlias;
        public string UserName;
    }

    #endregion

}