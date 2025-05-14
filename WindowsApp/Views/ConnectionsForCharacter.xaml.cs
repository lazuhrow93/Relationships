using System.Windows;
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

        public ConnectionsForCharacterViewModel ViewModel => _viewModel;

        public void SetConnectedCharacters(IEnumerable<Domain.Models.ConnectionsForCharacter.Dto> connectedCharacters)
        {
            ViewModel.AddCharacters(connectedCharacters);
        }
    }
}
