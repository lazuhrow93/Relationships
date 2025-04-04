using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data;

public interface IDataContextService
{
    Task<EntityEntry<TEntity>> Add<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

    int SaveChanges();
}

public class DataContextService : IDataContextService
{
    private readonly RelationshipDbContext _dbContext;

    public DataContextService(RelationshipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EntityEntry<TEntity>> Add<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
    {
        return await _dbContext.AddAsync(entity, cancellationToken);
    }

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }
}
