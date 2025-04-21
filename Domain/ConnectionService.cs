using Data.Operations;
using Data.Queries;
using Entities;
using Entities.Enums;

namespace Domain;

public interface IConnectionService
{
    Task<Connection?> CreateConnection(int characterOneId, int characterTwoId, ConnectionType connectionType, CancellationToken cancellationToken);
}

public class ConnectionService : IConnectionService
{
    private readonly ICrudOperator<Connection> _operations;
    private readonly ICharacterQueries _characters;

    public ConnectionService(ICrudOperator<Connection> operations,
        ICharacterQueries characters)
    {
        _operations = operations;
        _characters = characters;
    }

    public async Task<Connection?> CreateConnection(int characterOneId, int characterTwoId, ConnectionType connectionType, CancellationToken cancellationToken)
    {
        var characters = await _characters.FindMany(new HashSet<int>([characterOneId, characterTwoId]), cancellationToken);

        if(characters.Length != 2)
        {
            return null; // or throw an exception
        }

        var newConnection = new Connection()
        {
            SourceCharacterId = characterOneId,
            TargetCharacterId = characterTwoId,
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