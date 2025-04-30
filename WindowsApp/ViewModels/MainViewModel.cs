using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WindowsApp.Commands;
using WindowsApp.Domain.ApiAccess;
using WindowsApp.Domain.Models;
using WindowsApp.Views;

namespace WindowsApp.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    #region Model Fields

    private ObservableCollection<Character> _userCharacters { get; } = new();
    private Character? _selectedCharacter;

    #endregion

    #region Command Fields

    private ICommand _addCharacters;
    private ICommand _submitCharacterCommand;

    #endregion

    #region Service Fields

    private IRelationshipApplicationAccess _apiAccess;

    #endregion

    public MainViewModel(IRelationshipApplicationAccess apiAccess)
    {
        _apiAccess = apiAccess;
        _addCharacters = new RelayCommand(ShowAddCharacterWindow);
        _submitCharacterCommand = new RelayCommand(OnSubmitCharacter);
    }

    #region Model Properties

    public ObservableCollection<Character> UserCharacters => _userCharacters;

    public Character? SelectedCharacter
    {
        get => _selectedCharacter;
        set
        {
            _selectedCharacter = value;
            OnPropertyChanged(nameof(SelectedCharacter));
        }
    }

    #endregion

    #region Command/Event Properties

    public ICommand AddCharacter => _addCharacters;

    public ICommand SubmitCharacterCommand => _submitCharacterCommand;

    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Service Properties

    public IRelationshipApplicationAccess ApiAccess => _apiAccess;

    #endregion

    public void ShowAddCharacterWindow(object? obj)
    {
        AddCharacter window = new AddCharacter();
        window.Show();
    }

    public async Task LoadUserCharacters(int id, bool withConnections = false)
    {
        try
        {
            var characters = await ApiAccess.GetMyCharacters(id, true, CancellationToken.None);

            UserCharacters.Clear();

            foreach (var character in characters)
            {
                UserCharacters.Add(character);
            }
        }
        catch (Exception ex)
        {
            // Show a simple error dialog
            MessageBox.Show(
                ex.Message,
                "Error Loading Characters",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    public void OnSubmitCharacter(object? obj)
    {
        if (SelectedCharacter is not null)
        {
            int? userId = SelectedCharacter.UserId;
            // Now you can call your API
        }
    }

    #region Private Helpers

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}
