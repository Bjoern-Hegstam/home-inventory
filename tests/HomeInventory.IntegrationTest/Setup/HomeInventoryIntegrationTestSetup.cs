using HomeInventory.Database;
using HomeInventory.IntegrationTest.Framework.Core;
using HomeInventory.IntegrationTest.Framework.PostreSql;
using HomeInventory.IntegrationTest.Framework.WebHost;
using HomeInventory.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HomeInventory.IntegrationTest.Setup;

[CollectionDefinition(nameof(IntegrationTestCollection))]
public class IntegrationTestCollection : ICollectionFixture<HomeInventoryIntegrationTestFixture>;

public class HomeInventoryIntegrationTestFixture : IntegrationTestFixture<HomeInventoryIntegrationTestEnvironment>;

public class HomeInventoryIntegrationTestEnvironment(IServiceProvider testServices)
    : IntegrationTestEnvironment(testServices)
{
    public override List<IExternalDependency> CreateExternalDependencies() =>
    [
        new TestContainerPostgreSqlDb
        {
            Database = "home-inventory-db",
        }
    ];

    public override ISystemUnderTest CreateSystemUnderTest() => new HomeInventoryTestHost(TestServices);

    public override Task BeforeEachAsync()
    {
        var context = TestServices.GetRequiredService<StockItemContext>();
        context.StockItems.ExecuteDelete();

        return Task.CompletedTask;
    }
}

public class HomeInventoryTestHost(IServiceProvider serviceProvider) : WebHostSystemUnderTest<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var db = serviceProvider.GetRequiredService<IPostgreSqlDb>();
            services.AddDbContext<StockItemContext>(options => options.UseNpgsql(db.ConnectionString));

            ApplyDbMigrations(services);
        });
    }

    private static void ApplyDbMigrations(IServiceCollection services)
    {
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        using IServiceScope scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<StockItemContext>();
        context.Database.Migrate();
    }
}