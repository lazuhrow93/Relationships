using System.Security.Policy;
using System.Windows;
using WindowsApp.ViewModels;

namespace WindowsApp.Views;
/// <summary>
/// Interaction logic for AddCharacter.xaml
/// </summary>
public partial class AddCharacter : Window
{
    private AddCharacterViewModel _viewModel;

    public AddCharacter(AddCharacterViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;

        DataContext = _viewModel;
    }

    public AddCharacterViewModel ViewModel => _viewModel;

    public void SetUserId(int? userId)
    {
        _viewModel.SetUserId(userId);
    }
}
