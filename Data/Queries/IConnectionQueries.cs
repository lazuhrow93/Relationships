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
        if (options.Characters)
        {
            query =  query.Include(c => c.SourceCharacter)
                .Include(c => c.TargetCharacter);
        }

        if (options.RelationTypes)
        {
            query = query.Include(c => c.RelationType);
        }
        return query;
    }

    #endregion
}

public record struct CharacterQueryOptions(bool Characters, bool RelationTypes)
{
    public static CharacterQueryOptions IncludeCharacters => new(true, false);
    public static CharacterQueryOptions CharacterAndRelationType => new(true, true);
}