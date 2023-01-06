using System.Text.Json.Serialization;
using Domain.Models;
using Domain.UseCases;
using Domain.UseCases.CreateProductUseCase;
using Domain.UseCases.Ports;
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

    private readonly string _policyName = "CorsPolicy";

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(opt =>
        {
            opt.AddPolicy(name: _policyName, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddScoped<UseCase<Task<Product>, Product>, CreateProductUseCase>();
        services.AddScoped<UseCase<Task<Product>, Product>, UpdateProductUseCase>();
        services.AddScoped<UseCase<Task<Product>, Product>, DeleteProductUseCase>();
        services.AddScoped<UseCase<Task<Product>, Product>, GetProductUseCase>();
        services.AddScoped<UseCase<Task<List<Product>>, Product>, GetAllProductsUseCase>();

        services.AddScoped<UseCase<Task<Buy>, Buy>, PurchaseUseCase>();
        services.AddScoped<UseCase<Task<List<Buy>>, int>, ShowHistoryUseCase>();

        services.AddScoped<IProductRepository, ProductAdapter>();
        services.AddScoped<IBuyRepository, BuyAdapter>();

        services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer"),
            builder => builder.MigrationsAssembly("Infrastructure")).EnableSensitiveDataLogging());


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

        app.UseCors(_policyName);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}