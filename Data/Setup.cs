using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class Setup
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection)
    {
        var assembly = typeof(Setup).Assembly;

    }
}
