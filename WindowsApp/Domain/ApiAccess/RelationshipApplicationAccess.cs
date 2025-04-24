using System.Net.Http;
using WindowsApp.Domain.Models;
using WindowsApp.Setup;

namespace WindowsApp.Domain.ApiAccess;

public interface IRelationshipApplicationAccess
{
    Task<UserCharacters[]> GetMyCharacters(int userId, CancellationToken cancellationToken);
}

public class RelationshipApplicationAccess : IRelationshipApplicationAccess
{
    private readonly IRelationshipHttpClient _httpClient;

    public RelationshipApplicationAccess(IRelationshipHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserCharacters[]> GetMyCharacters(int userId, CancellationToken cancellationToken)
    {
        var userCharacters = await _httpClient.GetAsync<UserCharacters[]>(RelationshipUrls.GetUserCharacters(userId), cancellationToken);

        return userCharacters ?? [];
    }
}
