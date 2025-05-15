using System.Runtime.CompilerServices;
using Data.Operations;
using Data.Queries;
using Domain.Models.Entities;
using Entities;

namespace Domain;

public interface ICharacterService
{
    Task<Character?> CreateCharacter(CharacterModel model, CancellationToken cancellationToken);

    Task<List<Character>> FetchCharactersNotConnectedTo(int characterId, int userId, CancellationToken cancellationToken);
}

public class CharacterService : ICharacterService
{
    private readonly ICrudOperator<Character> _operations;

    private readonly ICharacterQueries _characters;

    public CharacterService(ICrudOperator<Character> operations,
        ICharacterQueries characters)
    {
        _operations = operations;
        _characters = characters;
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

    public async Task<List<Character>> FetchCharactersNotConnectedTo(int characterId, int userId, CancellationToken cancellationToken)
    {
        var userCharacters = await _characters.ForUser(userId, includeConnection: true, cancellationToken);
        var returnVal = new List<Character>();

        foreach (var testCharacter in userCharacters)
        {
            if(testCharacter.Id == characterId) //itself
            {
                continue;
            }

            if (HasNoConnections(testCharacter) || NotATargetOf(testCharacter, characterId))
            {
                returnVal.Add(testCharacter);
            }
        }

        return returnVal;
    }

    #region Private Helpers

    private bool HasNoConnections(Character character)
    {
        return character.SourceConnections.Count() == 0 && character.TargetConnections.Count() == 0;
    }

    private bool NotATargetOf(Character character, int characterId)
    {
        return character.TargetConnections.All(c => c.SourceCharacterId != characterId);
    }

    #endregion
}