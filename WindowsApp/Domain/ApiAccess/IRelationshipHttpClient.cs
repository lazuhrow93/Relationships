using System.Net.Http;
using System.Text.Json;
using WindowsApp.Setup;

namespace WindowsApp.Domain.ApiAccess;

public interface IRelationshipHttpClient
{
    Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken);
    Task<T?> GetAsync<T>(UriBuilder uriBuilder, CancellationToken cancellationToken);
}

public class RelationshipHttpClient : IRelationshipHttpClient
{
    private readonly HttpClient _httpClient;

    public RelationshipHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<T>(content);
    }

    public async Task<T?> GetAsync<T>(UriBuilder uriBuilder, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(uriBuilder.Uri, cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<T>(content);
    }
}