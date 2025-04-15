
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data;

public class MyEntity<T> where T : class
{
    private readonly EntityEntry<T> _dbEntry;

    public MyEntity(EntityEntry<T> entity)
    {
        _dbEntry = entity;
    }

    public bool IsAdded => _dbEntry.State == EntityState.Added;
}
