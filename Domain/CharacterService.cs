using Data.Operations;
using Data.Queries;
using Domain.Models.Entities;
using Entities;
using Microsoft.Identity.Client.Extensions.Msal;

namespace Domain;

public interface ICharacterService
{
    Task<Character?> CreateCharacter(CharacterModel model, CancellationToken cancellationToken);
}

public class CharacterService : ICharacterService
{
    private readonly ICrudOperator<Character> _operations;

    public CharacterService(ICrudOperator<Character> operations)
    {
        _operations = operations;
    }

    public async Task<Character?> CreateCharacter(CharacterModel model, CancellationToken cancellationToken)
    {
        var newCharacter = new Character()
        {
            UserId = model.UserId,
            Name = model.Name
        };

        var result = await _operations.AddAsync(newCharacter, cancellationToken);

        if (result.IsAdded)
        {
            await _operations.SaveChanges(cancellationToken);
        }

        return newCharacter;
    }
}