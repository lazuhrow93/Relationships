using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WindowsApp.Setup;
using WindowsApp.Views;

namespace WindowsApp;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Start host (optional if you need lifetime control)
        AppHost.Host.Start();

        // Resolve and show main window
        var mainWindow = AppHost.Services.GetRequiredService<Main>();
        mainWindow.Show();
    }
}

