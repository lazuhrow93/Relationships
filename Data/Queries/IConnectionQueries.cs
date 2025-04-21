using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Queries;

public interface IConnectionQueries : IEntityQueries<Connection>
{
    Task<Connection[]> GetConnectionsForCharacter(int characterId, CharacterQueryOptions options, CancellationToken cancellationToken);
}

public class ConnectionQueries : EntityQueries<Connection>, IConnectionQueries
{
    public ConnectionQueries(RelationshipDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Connection[]> GetConnectionsForCharacter(int characterId, CharacterQueryOptions options, CancellationToken cancellationToken)
    {
        var query = Query
            .Where(c => c.SourceCharacterId == characterId);

        return ApplyOptions(query, options).ToArrayAsync(cancellationToken);
    }

    #region Private Helpers

    private static IQueryable<Connection> ApplyOptions(IQueryable<Connection> query, CharacterQueryOptions options)
    {
        if (options.characters)
        {
            return query.Include(c => c.SourceCharacter)
                .Include(c => c.TargetCharacter);
        }
        return query;
    }

    #endregion
}

public record struct CharacterQueryOptions(bool characters)
{
    public static CharacterQueryOptions IncludeCharacters => new(true);
}