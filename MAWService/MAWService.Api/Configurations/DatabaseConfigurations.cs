using Microsoft.Extensions.Options;
using System;
using MAWService.Persistance;
using Microsoft.EntityFrameworkCore;

namespace MAWService.Api.Configurations;

public static class DatabaseConfigurations
{
    public static void SetupDatabase(this IServiceCollection services, string sqlDbConnectionString)
    {
        services.AddDbContext<AppDbContext>(b => b
            .UseLazyLoadingProxies()
            .UseSqlServer(sqlDbConnectionString, options => options.CommandTimeout(120)
            .EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
        ));

        using var appDbContext = services.BuildServiceProvider().GetRequiredService<AppDbContext>();
        appDbContext.Database.Migrate();
    }
}
