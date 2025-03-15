using HomeInventory.Database;
using HomeInventory.Domain.Integration;
using HomeInventory.Domain.Service;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.WebApi;

public partial class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        IServiceCollection services = builder.Services;

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();

        services.AddScoped<IStockItemService, StockItemService>();
        services.AddScoped<IStockItemRepository, EfStockItemRepository>();

        services.AddDbContext<StockItemContext>(options =>
        {
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
        });

        services.AddCors(options =>
        {
            if (builder.Environment.IsDevelopment())
            {
                options.AddDefaultPolicy(corsPolicyBuilder =>
                    corsPolicyBuilder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                );
            }
        });

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}

public partial class Program
{
    // Required for integration tests
}