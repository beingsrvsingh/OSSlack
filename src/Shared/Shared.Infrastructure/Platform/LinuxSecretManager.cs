
using Shared.Contracts.Interfaces;

namespace Shared.Infrastructure.Platform;

public class LinuxSecretManager : IPlatformService
{
    private readonly string _basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".linuxsecrets");

    public LinuxSecretManager()
    {
        Directory.CreateDirectory(_basePath);
    }

    public IEnumerable<string> GetAllCredentialKeys()
    {
        return Directory.Exists(_basePath)
            ? Directory.GetFiles(_basePath).Select(Path.GetFileName)
            : Enumerable.Empty<string>();
    }

    public void AddCredential(string keyName, string secret)
    {
        string file = GetSecretPath(keyName);
        File.WriteAllText(file, secret);
        File.SetAttributes(file, FileAttributes.Hidden); // optional
    }

    public string? GetCredential(string keyName)
    {
        string file = GetSecretPath(keyName);
        return File.Exists(file) ? File.ReadAllText(file) : null;
    }


    public void RemoveCredential(string keyName)
    {
        var filePath = GetPathForKey(keyName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    private string GetSecretPath(string keyName) =>
        Path.Combine(_basePath, $"{keyName}.secret");

    private string GetPathForKey(string keyName)
    {
        // Ensure no invalid filename characters are passed in keyName
        var sanitized = keyName.Replace(":", "_");
        return Path.Combine(_basePath, sanitized);
    }
}