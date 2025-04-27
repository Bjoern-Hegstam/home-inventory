using HomeInventory.IntegrationTest.Framework.Core;
using Testcontainers.PostgreSql;

namespace HomeInventory.IntegrationTest.Framework.PostreSql;

public class TestContainerPostgreSqlDb : TestContainerService<PostgreSqlContainer>, IPostgreSqlDb
{
    public string Database { get; init; } = "it-db";
    public string Username { get; init; } = "it-db-user";
    public string Password { get; init; } = "it-db-pwd";

    public string ConnectionString => Container.GetConnectionString();

    protected override PostgreSqlContainer CreateContainer()
    {
        return new PostgreSqlBuilder()
            .WithImage("postgres:16")
            .WithDatabase(Database)
            .WithUsername(Username)
            .WithPassword(Password)
            .Build();
    }
}