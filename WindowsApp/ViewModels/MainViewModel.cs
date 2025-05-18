using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
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
    private int _userId = 1;

    #endregion

    #region Command Fields

    private ICommand _addCharacters;
    private ICommand _viewCharacterConnectionsCommand;
    private ICommand _refreshWindowCommand;

    #endregion

    #region Service Fields

    private IServiceProvider _serviceProvider;
    private IRelationshipApplicationAccess _apiAccess;

    #endregion

    public MainViewModel(IRelationshipApplicationAccess apiAccess, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _apiAccess = apiAccess;
        _addCharacters = new RelayCommand(ShowAddCharacterWindow);
        _viewCharacterConnectionsCommand = new AsyncRelayCommand(OnSubmitViewCharacterConnections);
        _refreshWindowCommand = new AsyncRelayCommand(RefreshMyCharacters);
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
    public ICommand ViewCharacterConnectionsCommand => _viewCharacterConnectionsCommand;

    public ICommand RefreshWindowCommand => _refreshWindowCommand;

    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Service Properties

    public IServiceProvider ServiceProvider => _serviceProvider;
    public IRelationshipApplicationAccess ApiAccess => _apiAccess;

    #endregion

    public void ShowAddCharacterWindow(object? obj)
    {   
        var window = ServiceProvider.GetRequiredService<AddCharacter>();
        window.SetUserId(_userId);
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

    public async Task RefreshMyCharacters(object? obj)
    {
        await LoadUserCharacters(_userId);
    }

    public async Task OnSubmitViewCharacterConnections(object? obj)
    {
        if (SelectedCharacter is not null)
        {
            if (SelectedCharacter?.CharacterId is null)
            {
                MessageBox.Show("Please select a character first.");
                return;
            }

            // Now you can call your API
            var characterConnections = await ApiAccess.GetConnectionsForCharacters(SelectedCharacter.CharacterId!.Value, CancellationToken.None);
            var window = ServiceProvider.GetRequiredService<Views.ConnectionsForCharacter>();
            var initWindowTask = window.InitializeConnectionsForCharacter(SelectedCharacter, _userId);
            window.Show();
            await initWindowTask;
        }
    }

    #region Private Helpers

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}
