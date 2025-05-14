using System.Net.Http;
using System.Text.Json;

namespace WindowsApp.Domain.ApiAccess;

public interface IRelationshipHttpClient
{
    Task<T?> GetAsync<T>(UriBuilder uriBuilder, CancellationToken cancellationToken);
    Task PostAsync<TRequest>(Uri uriBuilder, TRequest request, CancellationToken cancellationToken);
}

public class RelationshipHttpClient : IRelationshipHttpClient
{
    private readonly HttpClient _httpClient;

    public RelationshipHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
     
    public async Task<TResponse?> GetAsync<TResponse>(UriBuilder uriBuilder, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(uriBuilder.Uri, cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<TResponse>(content);
    }

    public async Task PostAsync<TRequest>(Uri uriBuilder, TRequest request, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uriBuilder, content, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}