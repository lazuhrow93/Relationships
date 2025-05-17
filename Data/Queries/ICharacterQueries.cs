using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Queries;

public interface ICharacterQueries : IEntityQueries<Character>
{
    Task<Character[]> ForUser(int userId, bool? includeConnection, CancellationToken cancellationToken);
}

public class CharacterQueries : EntityQueries<Character>, ICharacterQueries
{
    public CharacterQueries(RelationshipDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Character[]> ForUser(int userId, bool? withConnections, CancellationToken cancellationToken)
    {
        var q = Query
            .Where(c => c.UserId == userId);
        if(withConnections == true)
        {
            q = q.Include(c => c.SourceConnections);
        }
        return q.ToArrayAsync(cancellationToken);
    }
}