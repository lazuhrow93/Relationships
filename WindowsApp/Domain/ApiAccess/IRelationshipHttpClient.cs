using System.Net.Http;
using System.Text.Json;
using WindowsApp.Setup;

namespace WindowsApp.Domain.ApiAccess;

public interface IRelationshipHttpClient
{
    Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken);
}

public class RelationshipHttpClient : IRelationshipHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;

    public RelationshipHttpClient(HttpClient httpClient,
        AppSettings appSettings)
    {
        _httpClient = httpClient;
        _appSettings = appSettings;

        if (string.IsNullOrWhiteSpace(appSettings.Host))
            throw new ArgumentException("Host cannot be null or empty", nameof(appSettings.Host));
        _httpClient.BaseAddress = new Uri(appSettings.Host);
    }

    public AppSettings Config => _appSettings;

    public async Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<T>(content);
    }
}