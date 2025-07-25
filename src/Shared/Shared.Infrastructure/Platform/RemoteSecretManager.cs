
using System.Net.Http.Json;
using System.Text;
using Shared.Contracts.Interfaces;

namespace Shared.Infrastructure.Platform;
public class RemoteSecretManager : IPlatform
{
    private readonly HttpClient _httpClient;

    public RemoteSecretManager()
    {
        _httpClient = new HttpClient();
    }

    public async Task<IEnumerable<string>> GetAllCredentialKeysAsync()
    {
        var response = await _httpClient.GetAsync("/api/secrets/keys");
        response.EnsureSuccessStatusCode();
        var keys = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        return keys ?? Enumerable.Empty<string>();
    }

    public async Task<string?> GetCredentialAsync(string keyName)
    {
        var response = await _httpClient.GetAsync($"/api/secrets/{keyName}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<bool> AddCredentialAsync(string keyName, string secret)
    {
        var content = new StringContent(secret, Encoding.UTF8, "text/plain");
        var response = await _httpClient.PostAsync($"/api/secrets/{keyName}", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveCredentialAsync(string keyName)
    {
        var response = await _httpClient.DeleteAsync($"/api/secrets/{keyName}");
        return response.IsSuccessStatusCode;
    }
}
