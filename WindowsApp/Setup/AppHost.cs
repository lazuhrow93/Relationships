using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WindowsApp.Domain.ApiAccess;
using WindowsApp.ViewModels;

namespace WindowsApp.Setup
{
    public static class AppHost
    {
        private static IHost? _host;
        public static IHost Host => _host ??= CreateHost();

        private static IHost CreateHost() =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Register configuration
                    var appSettings = new AppSettings
                    {
                        Scheme = "http",
                        Host = "localhost",
                        Port = 5001
                    };
                    services.AddSingleton(appSettings);

                    // Register HttpClient
                    services.AddHttpClient<IRelationshipHttpClient, RelationshipHttpClient>();

                    // Register access layer
                    services.AddSingleton<IRelationshipApplicationAccess, RelationshipApplicationAccess>();

                    // Register ViewModels
                    services.AddSingleton<MainViewModel>()
                        .AddScoped<ConnectionsForCharacterViewModel>()
                        .AddScoped<AddCharacterViewModel>();

                    // Register Windows
                    services.AddTransient<Views.Main>()
                        .AddTransient<Views.ConnectionsForCharacter>()
                        .AddTransient<Views.AddCharacter>();
                })
                .Build();

        public static IServiceProvider Services => Host.Services;
    }
}
