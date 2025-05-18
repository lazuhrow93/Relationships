using System.Windows;
using System.Windows.Input;
using WindowsApp.Commands;
using WindowsApp.Domain.ApiAccess;
using WindowsApp.Domain.RequestModels;

namespace WindowsApp.ViewModels;
public class AddCharacterViewModel
{
    #region Models Fields

    private string? _characterName;
    private int? _userId;

    #endregion

    #region Services Fields

    private readonly IRelationshipApplicationAccess _api;

    #endregion

    #region Command/Event Fields

    public ICommand _submitNewCharacter;

    #endregion

    public AddCharacterViewModel(IRelationshipApplicationAccess api)
    {
        _api = api;
        _submitNewCharacter = new AsyncRelayCommand(OnSubmitNewCharacter);
    }

    #region Models Properties

    public string? CharacterName
    {
        get => _characterName;
        set
        {
            _characterName = value;
        }
    }

    public string? CharacterDescription { get; set; }
    public int? UserId => _userId;

    #endregion

    #region Command/Event Properties

     public ICommand SubmitNewCharacter => _submitNewCharacter;

    #endregion

    public void SetUserId(int? userId)
    {
        _userId = userId;
    }

    public Task OnSubmitNewCharacter(object? obj)
    {
        if (CharacterName == null || UserId == null)
        {
            MessageBox.Show("Character Name and User ID cannot be null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return Task.CompletedTask;
        }
        var character = new CreateCharacterRequest
        {
            CharacterName = CharacterName,
            UserId = UserId.Value,
            Description = CharacterDescription
        };
        return _api.CreateCharacter(character, CancellationToken.None);
    }
}
