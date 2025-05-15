using System.Collections.ObjectModel;
using WindowsApp.Domain.ApiAccess;
using WindowsApp.Domain.Models;

namespace WindowsApp.ViewModels;

public class ConnectionsForCharacterViewModel
{
    private IRelationshipApplicationAccess _apiAccess;
    private ObservableCollection<ConnectionsForCharacter.Dto> _connectedCharacters { get; } = new();
    private ObservableCollection<DisconnectionsForCharacter.Dto> _disconnectedCharacters { get; } = new();

    private Character? _characterSpotlight = null;

    private string _defaultCharacterName = "unknown";
    private int _userId = -1;

    public ConnectionsForCharacterViewModel(IRelationshipApplicationAccess apiAccess)
    {
        _apiAccess = apiAccess;
    }

    public ObservableCollection<ConnectionsForCharacter.Dto> ConnectedCharacters => _connectedCharacters;
    public ObservableCollection<DisconnectionsForCharacter.Dto> DisconnectedCharacters => _disconnectedCharacters;

    public string CharacterName => _characterSpotlight?.CharacterName ?? _defaultCharacterName;

    public string ConnectCharacterToLabel => $"Connect a Character to {CharacterName}";

    public async Task InitWindow(int userId, Character character)
    {
        _connectedCharacters.Clear();
        _disconnectedCharacters.Clear();
        _characterSpotlight = character;
        _userId = userId;
        var connectionsToCharacter = await _apiAccess.GetConnectionsForCharacters(character.CharacterId!.Value, CancellationToken.None) ?? new();
        foreach(var connection in connectionsToCharacter!.Connections)
        {
            _connectedCharacters.Add(connection);
        }

        var nonConnectedToCharacter = await _apiAccess.GetNonConnectedCharacters(_userId, character.CharacterId!.Value, CancellationToken.None) ?? new();
        foreach (var nonConnection in nonConnectedToCharacter!.Disconnections)
        {
            _disconnectedCharacters.Add(nonConnection);
        }
    }
}