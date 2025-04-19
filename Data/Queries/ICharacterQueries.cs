using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Queries;

public interface ICharacterQueries : IEntityQueries<Character>
{
    Task<Character[]> ForUser(int userId, CancellationToken cancellationToken);
}

public class CharacterQueries : EntityQueries<Character>, ICharacterQueries
{
    public CharacterQueries(RelationshipDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Character[]> ForUser(int userId, CancellationToken cancellationToken)
    {
        return Query.Where(c => c.UserId == userId).ToArrayAsync(cancellationToken);
    }
}
