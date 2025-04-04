using Entities;

namespace Data.Queries;

public interface IEntityQueries { }

public interface IEntityQueries<TEntity> : IEntityQueries
    where TEntity : Entity
{
    Task<TEntity[]> GetUsersCharacters(int userId, CancellationToken cancellationToken);
}
