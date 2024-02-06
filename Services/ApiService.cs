using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IMemoryCache _memoryCache;

    public ApiService(HttpClient httpClient, IMemoryCache memoryCache)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        if (_memoryCache.TryGetValue(endpoint, out T cachedResult))
        {
            return cachedResult;
        }

        var response = await _httpClient.GetAsync(endpoint);

        response.EnsureSuccessStatusCode();

        var contentStream = await response.Content.ReadAsStreamAsync();

        var result = await JsonSerializer.DeserializeAsync<T>(contentStream, _jsonSerializerOptions);

        // Almacenar en caché el resultado con una expiración de 10 minutos
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(5) // Puedes ajustar según tus necesidades.
        };

        _memoryCache.Set(endpoint, result, cacheEntryOptions);

        return result;
    }
}