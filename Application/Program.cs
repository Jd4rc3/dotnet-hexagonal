using Api;
using Domain.UseCases;
using Domain.UseCases.Ports;
using Infrastructure;
using Infrastructure.Adapters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    services.GetRequiredService<Startup>().StartAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddSingleton<Startup>(sp => new Startup(args, dependencies =>
                {
                    dependencies.AddScoped<IProductRepository, ProductAdapter>();
                    dependencies.AddScoped<IBuyRepository, BuyAdapter>();
                    dependencies.AddDbContext<Context>(options => options.UseSqlServer(
                        "Server=127.0.0.1,1433;Database=store;User Id=sa;Password=Arc33456$;TrustServerCertificate=True;",
                        builder => builder.MigrationsAssembly("Infrastructure")).EnableSensitiveDataLogging());
                }
            ));
        });