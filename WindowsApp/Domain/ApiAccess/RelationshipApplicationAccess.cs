using System.Net.Http;
using WindowsApp.Domain.Models;
using WindowsApp.Setup;

namespace WindowsApp.Domain.ApiAccess;

public interface IRelationshipApplicationAccess
{
    Task<Character[]> GetMyCharacters(int userId, bool withConnections, CancellationToken cancellationToken);
}

public class RelationshipApplicationAccess : IRelationshipApplicationAccess
{
    private readonly IRelationshipHttpClient _httpClient;

    public RelationshipApplicationAccess(IRelationshipHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Character[]> GetMyCharacters(int userId, bool withConnections, CancellationToken cancellationToken)
    {
        try
        {
            var userCharacters = await _httpClient.GetAsync<Character[]>(RelationshipUrls.GetUserCharacters(userId), cancellationToken);
            return userCharacters ?? [];
        }
        catch (Exception e)
        {
            return [];
        }
    }
}
