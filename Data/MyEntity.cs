
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data;

public interface IMyEntity<T> where T : Entity
{
    bool IsAdded { get; }
    T Entity { get; set; }
}

public class MyEntity<T> : IMyEntity<T>
    where T : Entity
{
    private readonly EntityEntry<T> _dbEntry;

    public MyEntity(EntityEntry<T> entity)
    {
        _dbEntry = entity;
    }

    public bool IsAdded => _dbEntry.State == EntityState.Added;
    public T Entity => _dbEntry.Entity;
}
