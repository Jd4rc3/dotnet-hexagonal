using System.Text.Json.Serialization;
using Domain.Models;
using Domain.UseCases;
using Domain.UseCases.CreateProductUseCase;
using Domain.UseCases.Ports;

namespace Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    private readonly string _policyName = "CorsPolicy";

    private readonly WebApplication _app;

    public Startup(string[] args, Action<IServiceCollection> dependencies)
    {
        var options = new WebApplicationOptions()
        {
             ApplicationName = "Api",
             ContentRootPath = Path.GetFullPath("../../../../Api"),
             EnvironmentName = "Development",
             WebRootPath = Path.GetFullPath("../../../../Api"),
        };
        
        var builder = WebApplication.CreateBuilder(options);
        _configuration = builder.Configuration;
        
        dependencies.Invoke(builder.Services);
        ConfigureServices(builder.Services);

        _app = builder.Build();
        ConfigureApp(_app, _app.Environment);
    }

    private void ConfigureApp(WebApplication app, IWebHostEnvironment env)
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

    private void ConfigureServices(IServiceCollection services)
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


        services.AddAutoMapper(typeof(Startup));

        services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });
        
        
        // services.AddDbContext<Context>(options =>
        //     options.UseSqlServer(
        //             "Server=127.0.0.1,1433;Database=store;User Id=sa;Password=Arc33456$;TrustServerCertificate=True;")
        //         .EnableSensitiveDataLogging());
    }

    public void StartAsync()
    {
        _app.Run();
    }
}