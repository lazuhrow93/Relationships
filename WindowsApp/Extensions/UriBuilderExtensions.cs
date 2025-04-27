using WindowsApp.Setup;

namespace WindowsApp.Extensions;

public static class UriBuilderExtensions
{
    public static UriBuilder WithHost(this UriBuilder uriBuilder, AppSettings appSettings)
    {
        uriBuilder.Scheme = appSettings.Scheme;
        uriBuilder.Host = appSettings.Host;
        if(appSettings.Port != null) uriBuilder.Port = appSettings.Port!.Value;
        return uriBuilder;
    }
}
