using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MAWService.Api.Configurations;

public static class SwaggerConfigurations
{
    public static void ConfigureSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "MAW - API",
                Description = "MAW - Meet After Work - Backend site of application to schedule time after work",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}
