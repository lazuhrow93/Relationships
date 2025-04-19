using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Queries;

public interface IConnectionQueries : IEntityQueries<Connection>
{
    Task<Connection[]> GetConnectionsForCharacter(int characterId, CancellationToken cancellationToken);
}

public class ConnectionQueries : EntityQueries<Connection>, IConnectionQueries
{
    public ConnectionQueries(RelationshipDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Connection[]> GetConnectionsForCharacter(int characterId, CancellationToken cancellationToken)
    {
        return Query
            .Where(c => c.CharacterOneId == characterId)
            .ToArrayAsync(cancellationToken);
    }
}