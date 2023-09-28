using MAWService.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace MAWService.Api.Configurations;

public static class ExceptionMiddlewareConfigurations
{
    public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder applicationBuilder, IWebHostEnvironment env)
    {
        applicationBuilder.UseExceptionHandler(e =>
        {
            e.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature is not null)
                {
                    var exception = contextFeature.Error;
                    if (exception is not CustomException)
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = "application/json";

                        Console.WriteLine($"Something went wrong: {contextFeature.Error}");

                        var responseBody = env.IsDevelopment() ?
                                              new BaseExceptionModel(context.Response.StatusCode, exception.Message, exception.StackTrace, exception.InnerException?.Message) :
                                              new BaseExceptionModel(context.Response.StatusCode, "Server error");

                        await context.Response.WriteAsJsonAsync(responseBody);
                    }
                }
            });
        });
        return applicationBuilder;
    }
}
