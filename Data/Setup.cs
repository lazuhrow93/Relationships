using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class Setup
{
    public static IServiceCollection AddDataLayer(this IServiceCollection serviceCollection, DataLayerSettings? dbSettings)
    {
        if (dbSettings == null)
        {
            throw new ArgumentNullException(nameof(dbSettings), "Database settings cannot be null");
        }
        return serviceCollection.AddDatabase(dbSettings);
    }

    private static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, DataLayerSettings dbSettings)
    {
        return serviceCollection.AddDbContext<RelationshipDbContext>(opt => opt.UseSqlServer(dbSettings.ConnectionString));
    }
}

public class DataLayerSettings
{
    public string? ConnectionString { get; set; }
}