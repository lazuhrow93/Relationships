using Azure.Identity;
using Data;
using Microsoft.Extensions.Options;

namespace Api.Configuration;

public static class Setup
{
    public static IServiceCollection SetupApplication(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var dbConnectionString = builder.Configuration["AppConfig:ConnectionString"] ?? throw new Exception("No connection string");

        builder.Services.AddDataLayer(new DataLayerSettings()
        {
            ConnectionString = dbConnectionString
        });
        return services;
    }

    //below is the method to connect to Azure Portal for the settings.
    public static IServiceCollection SetupApplicationForAzure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        // Bind Azure App Configuration to Configuration
        string endpoint = builder.Configuration["AppConfig:ConnectionString"] ?? throw new Exception("No user secrets");
        builder.Configuration.AddAzureAppConfiguration(endpoint); //id rather go through the route of providing a userid and password but this is fine.

        // Bind configuration to AppSettings
        var appsettings = builder.Configuration.Get<AppSettings>();
        if (appsettings == null)
        {
            throw new Exception("No appsettings");
        }
        // Now pass DataLayerSettings to AddDatabase
        builder.Services.AddDataLayer(new DataLayerSettings()
        {
            ConnectionString = appsettings.ConnectionString
        });

        return services;
    }
}

public class AppSettings
{
    public string? ConnectionString { get; set; }
}
