using System.Collections.ObjectModel;
using WindowsApp.Domain.Models;

namespace WindowsApp.ViewModels;

public class ConnectionsForCharacterViewModel
{
    private ObservableCollection<ConnectionsForCharacter.Dto> _characters { get; } = new();

    public ConnectionsForCharacterViewModel()
    {
    }

    public ObservableCollection<ConnectionsForCharacter.Dto> Characters => _characters;

    public void AddCharacters(IEnumerable<ConnectionsForCharacter.Dto> characters)
    {
        _characters.Clear();
        foreach (var character in characters)
        {
            _characters.Add(character);
        }
    }
}