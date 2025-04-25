using System.Windows;
using Accessibility;
using WindowsApp.ViewModels;

namespace WindowsApp.Views;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class Main : Window
{
    private MainViewModel _mainViewModel;

    public Main(MainViewModel mainViewModel)
    {
        InitializeComponent();
        _mainViewModel = mainViewModel;

        DataContext = _mainViewModel;
        Loaded += async (_, __) => { await _mainViewModel.LoadUserCharacters(1); };
    }

    public MainViewModel MainViewModel => _mainViewModel;
}