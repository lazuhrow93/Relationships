using Data;
using Data.Queries;
using Domain.Models.Entities;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public interface IOrchestrator
{  
    Task<AppServiceResult<TEntity>> GetAll<TEntity>(int id, CancellationToken cancellationToken) where TEntity : Entity;
    Task<AppServiceResult<Character>> CreateCharacter(CharacterModel character, CancellationToken cancellationToken);
}

public class Orchestrator : IOrchestrator
{
    private readonly Dictionary<Type, IEntityQueries> _queries;
    private readonly IDataContextService _dataContext;

    public Orchestrator(IEntityQueries<Character> characterQueries,
        IDataContextService dataContext)
    {
        _queries = new();
        _queries.Add(typeof(Character), characterQueries);

        _dataContext = dataContext;
    }

    public async Task<AppServiceResult<TEntity>> GetAll<TEntity>(int id, CancellationToken cancellationToken)
        where TEntity : Entity
    {
        if(_queries.TryGetValue(typeof(TEntity), out var query) == false ||
            query is not IEntityQueries<TEntity> entityQuery)
        {
            return new AppServiceResult<TEntity>([], false, $"No registerd query for {typeof(TEntity).Name} registered");
        }

        var result = await entityQuery.GetUsersCharacters(id, cancellationToken);
        return new AppServiceResult<TEntity>(result, true, null);
    }

    public async Task<AppServiceResult<Character>> CreateCharacter(CharacterModel model, CancellationToken cancellationToken)
    {
        var character = new Character()
        {
            UserId = model.UserId,
            Name = model.Name
        };
        
        var result = await _dataContext.Add(character, cancellationToken);

        if(result.State == EntityState.Added)
        {
            _dataContext.SaveChanges();
        }

        return new AppServiceResult<Character>([result.Entity], true, null);
    }
}

public record struct AppServiceResult<TEntity>(TEntity[] result, bool failure, string? message);