using Data.Operations;
using Data.Queries;
using Microsoft.EntityFrameworkCore;
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
        return serviceCollection
            .AddDatabase(dbSettings)
            .AddDataThings();
    }

    private static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, DataLayerSettings dbSettings)
    {
        return serviceCollection.AddDbContext<RelationshipDbContext>(opt => opt.UseSqlServer(dbSettings.ConnectionString));
    }

    private static IServiceCollection AddDataThings(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped(typeof(ICrudOperator<>), typeof(CrudOperations<>))
            .AddScoped<IConnectionQueries, ConnectionQueries>()
            .AddScoped<ICharacterQueries, CharacterQueries>();
    }
}

public class DataLayerSettings
{
    public string? ConnectionString { get; set; }
}