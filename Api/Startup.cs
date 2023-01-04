using System.Text.Json.Serialization;
using Domain.Models;
using Domain.UseCases;
using Domain.UseCases.CreateProductUseCase;
using Domain.UseCases.CreateProductUseCase.Ports;
using Domain.UseCases.DeleteProductUseCase;
using Domain.UseCases.GetProductUseCase;
using Domain.UseCases.UpdateProductUseCase;
using Infrastructure;
using Infrastructure.Adapters;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<UseCase<Task<Product>, Product>, CreateProductUseCase>();
        services.AddScoped<UseCase<Task<Product>, Product>, UpdateProductUseCase>();
        services.AddScoped<UseCase<Task<Product>, Product>, DeleteProductUseCase>();
        services.AddScoped<UseCase<Task<Product>, Product>, GetProductUseCase>();
        services.AddScoped<UseCase<Task<List<Product>>, Product>, GetAllProductsUseCase>();

        services.AddScoped<IProductRepository, ProductAdapter>();

        services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer"),
            builder => builder.MigrationsAssembly("Infrastructure")));

        services.AddAutoMapper(typeof(Startup));
        
        services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; }); 
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}