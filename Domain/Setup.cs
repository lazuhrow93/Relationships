using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class Setup
{
    public static IServiceCollection AddDomainLayer(this IServiceCollection services)
    {
        return services
            .AddScoped<ICharacterService, CharacterService>()
            .AddScoped<IConnectionService, ConnectionService>();
    }
}
