using System.Collections.ObjectModel;
using System.Windows.Input;
using WindowsApp.Commands;
using WindowsApp.Domain.ApiAccess;
using WindowsApp.Domain.Models;
using WindowsApp.Views;

namespace WindowsApp.ViewModels;

public class MainViewModel
{
    private ObservableCollection<Character> _userCharacters { get; } = new();
    private ICommand _addCharacters;

    private IRelationshipApplicationAccess _apiAccess;

    public MainViewModel(IRelationshipApplicationAccess apiAccess)
    {
        _apiAccess = apiAccess;
        _addCharacters = new RelayCommand(ShowAddCharacterWindow);
    }

    public ObservableCollection<Character> UserCharacters => _userCharacters;
    public ICommand AddCharacter => _addCharacters;

    public void ShowAddCharacterWindow(object? obj)
    {
        AddCharacter window = new AddCharacter();
        window.Show();
    }

    public async Task LoadUserCharacters(int id, bool withConnections = false)
    {
        var characters = await _apiAccess.GetMyCharacters(id, true, CancellationToken.None);

        UserCharacters.Clear();

        foreach (var character in characters)
        {
            UserCharacters.Add(character);
        }
    }
}
