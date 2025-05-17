using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Queries;

public interface IEntityQueries { }

public interface IEntityQueries<TEntity> : IEntityQueries
    where TEntity : Entity
{
    Task<TEntity[]> GetAll(CancellationToken cancellationToken);

    Task<TEntity?> Find(int id, CancellationToken cancellationToken);

    Task<TEntity[]> FindMany(HashSet<int> hashSet, CancellationToken cancellationToken);
}

public class EntityQueries<TEntity> : IEntityQueries<TEntity>
    where TEntity : Entity
{
    private readonly RelationshipDbContext _dbContext;
    
    public EntityQueries(RelationshipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> Query => _dbContext.Set<TEntity>().AsQueryable();

    public Task<TEntity[]> GetAll(CancellationToken cancellationToken)
    {
        return Query.ToArrayAsync(cancellationToken);
    }

    public Task<TEntity?> Find(int id, CancellationToken cancellationToken)
    {
        return Query
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public Task<TEntity[]> FindMany(HashSet<int> hashSet, CancellationToken cancellationToken)
    {
        return Query
            .Where(e => hashSet.Contains(e.Id))
            .ToArrayAsync(cancellationToken);
    }
}
