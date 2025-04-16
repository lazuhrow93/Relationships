using Entities;

namespace Data.Queries;

public interface IEntityQueries { }

public interface IEntityQueries<TEntity> : IEntityQueries
    where TEntity : Entity
{
    Task<TEntity[]> FindMany(HashSet<int> hashSet, CancellationToken cancellationToken);
    Task<TEntity[]> GetUsersCharacters(int userId, CancellationToken cancellationToken);
}
