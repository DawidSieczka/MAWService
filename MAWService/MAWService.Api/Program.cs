using Microsoft.AspNetCore.Hosting;

namespace MAWService.Api;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });

    //private static void Main(string[] args)
    //{

    //    var builder = WebApplication.CreateBuilder(args);

    //    // Add services to the container.

    //    builder.Services.AddControllers();

    //    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //    builder.Services.AddEndpointsApiExplorer();
    //    builder.Services.AddSwaggerGen();

    //    var app = builder.Build();

    //    // Configure the HTTP request pipeline.
    //    if (app.Environment.IsDevelopment())
    //    {
    //        app.UseSwagger();
    //        app.UseSwaggerUI();
    //    }

    //    app.UseHttpsRedirection();

    //    app.UseAuthorization();

    //    app.MapControllers();

    //    app.Run();
    //}
}