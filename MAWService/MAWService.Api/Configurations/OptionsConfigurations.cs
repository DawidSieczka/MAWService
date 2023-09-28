using MAWService.Application.Common.Options;

namespace MAWService.Api.Configurations;

public static class OptionsConfigurations
{
    /// <summary>
    /// Options pattern - maps configurations from appsettings to strong typed objects
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.ConnectionStrings));
    }
}
