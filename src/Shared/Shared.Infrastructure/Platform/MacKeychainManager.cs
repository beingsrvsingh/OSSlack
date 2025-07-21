using System.Diagnostics;
using System.Text.RegularExpressions;
using Shared.Contracts.Interfaces;

namespace Shared.Infrastructure.Platform;

public class MacKeychainManager : IPlatformService
{

    public IEnumerable<string> GetAllCredentialKeys()
    {
        var keys = new List<string>();

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/usr/bin/security",
                Arguments = "find-generic-password -g",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        string output = process.StandardError.ReadToEnd(); // service name comes here
        process.WaitForExit();

        var matches = Regex.Matches(output, @"(?<=^ *""svce""<blob>="")[^""]+", RegexOptions.Multiline);
        foreach (Match match in matches)
        {
            keys.Add(match.Value);
        }

        return keys.Distinct();
    }

    public string? GetCredential(string keyName) => GetMacKeychainSecret(keyName);

    public void AddCredential(string keyName, string secret)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "/usr/bin/security",
                Arguments = $"add-generic-password -a {Environment.UserName} -s {keyName} -w {secret} -U",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            var proc = Process.Start(psi);
            proc?.WaitForExit();
        }
        catch
        {
            // log or handle failure
        }
    }

    #region macOS Keychain (via `security` CLI)
    private static string? GetMacKeychainSecret(string serviceName)
    {
        try
        {
            using var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/usr/bin/security",
                    Arguments = $"find-generic-password -s {serviceName} -w",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            proc.Start();
            string output = proc.StandardOutput.ReadToEnd().Trim();
            proc.WaitForExit();

            return proc.ExitCode == 0 ? output : null;
        }
        catch
        {
            return null;
        }
    }

    public void RemoveCredential(string keyName)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/usr/bin/security",
                Arguments = $"delete-generic-password -s {keyName}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();
        process.WaitForExit();
    }

    #endregion
}