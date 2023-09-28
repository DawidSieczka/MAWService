using MAWService.Api.Configurations;
using MAWService.Application.Common.Helpers;
using MAWService.Application.Common.Options;
using MAWService.Application.Operations.Users.Queries;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json.Serialization;
using MAWService.Application;
using MAWService.Application.Common.Exceptions;

namespace MAWService.Api;

internal class Startup
{
    private readonly IConfiguration _config;
    private IHostEnvironment _env { get; set; }

    public Startup(IConfiguration config)
    {
        _config = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {


        var serviceProvider = services.BuildServiceProvider();
        var connectionStrings = serviceProvider.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;
        services.SetupDatabase(connectionStrings.SqlDatabase);
        
        services.RegisterApplicationDependencies();
        
        services.AddControllers(options =>
        {
            options.Filters.Add(new HttpResponseExceptionFilter(serviceProvider.GetRequiredService<IHostEnvironment>().IsDevelopment()));
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        var domainAssembly = typeof(GetUserByExternalIdQuery).GetTypeInfo().Assembly;
        services.AddFluentValidation(new[] { domainAssembly });
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<GetUserByExternalIdQuery>());

        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.ConfigureSwaggerGen();
        services.AddConfigurationOptions(_config);

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        _env = env;

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MAWService.Api v1");
                c.RoutePrefix = string.Empty;
            });
        }

        //app.ConfigureExceptionHandler(env);

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}