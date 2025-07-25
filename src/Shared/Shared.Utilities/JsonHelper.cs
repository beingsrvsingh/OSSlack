using System.Text.Json;

namespace Shared.Utilities;


public static class JsonSerializerWrapper
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static T? Deserialize<T>(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return default;

        return JsonSerializer.Deserialize<T>(json, Options);
    }

    public static TValue? Deserialize<TValue>(Stream utf8Json, JsonSerializerOptions? options = null)
    {
        if (utf8Json == null)
            throw new ArgumentNullException(nameof(utf8Json));

        return JsonSerializer.Deserialize<TValue>(utf8Json, options ?? options);
    }

    public static string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }
}
