using Data.Queries;
using Data.Repository;
using Entities;
using Entities.Enums;

namespace Domain;

public interface IRelationshipService
{
    Task<Connection?> CreateConnection(int characterOneId, int characterTwoId, ConnectionType connectionType, CancellationToken cancellationToken);
}

public class RelationshipService : IRelationshipService
{
    private readonly ICrudOperator<Connection> _operations;
    private readonly IEntityQueries<Character> _characters;

    public RelationshipService(ICrudOperator<Connection> operations,
        IEntityQueries<Character> characters)
    {
        _operations = operations;
        _characters = characters;
    }

    public async Task<Connection?> CreateConnection(int characterOneId, int characterTwoId, ConnectionType connectionType, CancellationToken cancellationToken)
    {
        var characters = await _characters.FindMany(new HashSet<int>([characterOneId, characterTwoId]));

        if(characters.Length != 2)
        {
            return null; // or throw an exception
        }

        var newConnection = new Connection()
        {
            CharacterOneId = characterOneId,
            CharacterTwoId = characterTwoId,
            ConnectionType = connectionType
        };

        var result = await _operations.AddAsync(newConnection, cancellationToken);
        if (result.IsAdded)
        {
            await _operations.SaveChanges(cancellationToken);
        }
        return newConnection;
    }
}