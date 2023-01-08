using Domain.UseCases;
using Domain.UseCases.Ports;
using Infrastructure;
using Infrastructure.Adapters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var api = new ApiAdapter(args, config => { }, options =>
{
        
    options.AddScoped<IProductRepository, ProductAdapter>();
    options.AddScoped<IBuyRepository, BuyAdapter>();

    // options.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer"),
    //     builder => builder.MigrationsAssembly("Infrastructure")).EnableSensitiveDataLogging());
});
api.StartAsync();