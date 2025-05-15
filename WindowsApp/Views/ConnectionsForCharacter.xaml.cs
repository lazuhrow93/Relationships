using System.Windows;
using WindowsApp.Domain.Models;
using WindowsApp.ViewModels;

namespace WindowsApp.Views
{
    /// <summary>
    /// Interaction logic for ConnectionsForCharacter.xaml
    /// </summary>
    public partial class ConnectionsForCharacter : Window
    {
        private ConnectionsForCharacterViewModel _viewModel;

        public ConnectionsForCharacter(ConnectionsForCharacterViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        public Task InitializeConnectionsForCharacter(Character mainCharacter, int userId)
        {
            return _viewModel.InitWindow(userId, mainCharacter);
        }
    }
}
