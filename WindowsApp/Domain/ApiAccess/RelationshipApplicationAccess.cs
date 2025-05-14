using System.Net.Http;
using WindowsApp.Domain.Models;
using WindowsApp.Domain.RequestModels;
using WindowsApp.Extensions;
using WindowsApp.Setup;

namespace WindowsApp.Domain.ApiAccess;

public interface IRelationshipApplicationAccess
{
    Task<Character[]> GetMyCharacters(int userId, bool withConnections, CancellationToken cancellationToken);
    Task<ConnectionsForCharacter?> GetConnectionsForCharacters(int characterId, CancellationToken cancellationToken);
    Task CreateCharacter(CreateCharacterRequest character, CancellationToken cancellationToken);
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

    public async Task<ConnectionsForCharacter?> GetConnectionsForCharacters(int characterId, CancellationToken cancellationToken)
    {
        var uriBuilder = new UriBuilder();
        uriBuilder.WithHost(Config);
        uriBuilder.Path = RelationshipUrls.GetConnectionsForCharacter(characterId);
        var characterConnections = await _httpClient.GetAsync<ConnectionsForCharacter>(uriBuilder, cancellationToken);
        return characterConnections;

    }

    public async Task<Character[]> GetMyCharacters(int userId, bool withConnections, CancellationToken cancellationToken)
    {
        var uriBuilder = new UriBuilder();
        uriBuilder.WithHost(Config);
        uriBuilder.Path = RelationshipUrls.GetUserCharacters(userId);
        uriBuilder.Query = RelationshipUrls.WithConnections(withConnections);
        var userCharacters = await _httpClient.GetAsync<Character[]>(uriBuilder, cancellationToken);
        return userCharacters ?? [];
    }

    public async Task CreateCharacter(CreateCharacterRequest characterName, CancellationToken cancellationToken)
    {
        var uriBuilder = new UriBuilder();
        uriBuilder.WithHost(Config);
        uriBuilder.Path = RelationshipUrls.CreateCharacter;
        await _httpClient.PostAsync(uriBuilder.Uri, characterName, cancellationToken);
    }
}
