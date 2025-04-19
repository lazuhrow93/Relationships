using Data.Queries;
using Entities;

namespace Data.Operations;

public interface ICrudOperator<T> 
    where T : Entity
{
    Task<IMyEntity<T>> AddAsync(T entity, CancellationToken cancellationToken);
    Task<IMyEntity<T>> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<IMyEntity<T>> DeleteAsync(T entity, CancellationToken cancellationToken);
    Task SaveChanges(CancellationToken cancellationToken);
}

public class CrudOperations<T> : ICrudOperator<T> 
    where T : Entity
{
    private readonly RelationshipDbContext _dbContext;

    public CrudOperations(RelationshipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<IMyEntity<T>> AddAsync(T entity, CancellationToken cancellationToken)
    {
        var result = _dbContext.Add(entity);
        return Task.FromResult<IMyEntity<T>>(new MyEntity<T>(result));
    }

    public Task<IMyEntity<T>> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        var result = _dbContext.Remove(entity);
        return Task.FromResult<IMyEntity<T>>(new MyEntity<T>(result));
    }

    public Task<IMyEntity<T>> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        var result = _dbContext.Update(entity);
        return Task.FromResult<IMyEntity<T>>(new MyEntity<T>(result));
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
