using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WindowsApp.Setup;

public static class AppHost
{
    public static IHost? Host { get; private set; }

    public static void Init()
    {
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Register your services here
                services.AddSingleton<ApiService>();
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>(provider =>
                {
                    return new MainWindow
                    {
                        DataContext = provider.GetRequiredService<MainViewModel>()
                    };
                });
            })
            .Build();
    }

    public static T GetService<T>() where T : notnull
    {
        if(Host == null)
        {
            throw new Exception("Host was not setup correctly");
        }
        return Host.Services.GetRequiredService<T>();
    }
}
