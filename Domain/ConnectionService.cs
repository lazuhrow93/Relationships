using Data.Operations;
using Data.Queries;
using Entities;
using Entities.Enums;

namespace Domain;

public interface IConnectionService
{
    Task<Connection?> CreateConnection(int characterOneId, int characterTwoId, ConnectionType connectionType, string? Note, CancellationToken cancellationToken);
}

public class ConnectionService : IConnectionService
{
    private readonly ICrudOperator<Connection> _connectionOperations;
    private readonly ICrudOperator<ConnectionNote> _noteOperations;
    private readonly ICharacterQueries _characters;

    public ConnectionService(ICrudOperator<Connection> connectionOps,
        ICrudOperator<ConnectionNote> connectionNoteOps,
        ICharacterQueries characters)
    {
        _connectionOperations = connectionOps;
        _noteOperations = connectionNoteOps;
        _characters = characters;
    }

    public async Task<Connection?> CreateConnection(int characterOneId, int characterTwoId, ConnectionType connectionType, string? note, CancellationToken cancellationToken)
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

        var result = await _connectionOperations.AddAsync(newConnection, cancellationToken);
        var shouldSave = result.IsAdded;

        if (note is not null)
        {
            var noteResult = await _noteOperations.AddAsync(new ConnectionNote()
            {
                Content = note,
                ConnectionId = result.Entity.Id
            }, cancellationToken);
            shouldSave = shouldSave || noteResult.IsAdded;
        }

        if (shouldSave)
        {
            await _noteOperations.SaveChanges(cancellationToken);
        }
        return newConnection;
    }
}