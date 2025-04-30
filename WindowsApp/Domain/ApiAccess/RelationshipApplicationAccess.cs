using System.Net.Http;
using WindowsApp.Domain.Models;
using WindowsApp.Extensions;
using WindowsApp.Setup;

namespace WindowsApp.Domain.ApiAccess;

public interface IRelationshipApplicationAccess
{
    Task<Character[]> GetMyCharacters(int userId, bool withConnections, CancellationToken cancellationToken);
}

public class RelationshipApplicationAccess : IRelationshipApplicationAccess
{
    private readonly IRelationshipHttpClient _httpClient;
    private readonly AppSettings _appSettings;
    
    public RelationshipApplicationAccess(IRelationshipHttpClient httpClient,
        AppSettings appSettings)
    {
        _httpClient = httpClient;
        _appSettings = appSettings;
    }

    public AppSettings Config => _appSettings;

    public async Task<Character[]> GetMyCharacters(int userId, bool withConnections, CancellationToken cancellationToken)
    {
        var uriBuilder = new UriBuilder();
        uriBuilder.WithHost(Config);
        uriBuilder.Path = RelationshipUrls.GetUserCharacters(userId);
        uriBuilder.Query = RelationshipUrls.WithConnections(withConnections);
        var userCharacters = await _httpClient.GetAsync<Character[]>(uriBuilder, cancellationToken);
        return userCharacters ?? [];
    }
}
