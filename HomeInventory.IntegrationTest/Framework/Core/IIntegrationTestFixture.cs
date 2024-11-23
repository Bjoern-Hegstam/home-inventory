using Xunit;

namespace HomeInventory.IntegrationTest.Framework.Core;

public interface IIntegrationTestFixture : IServiceProvider, IAsyncLifetime
{
    public Task BeforeEachAsync();
    public Task AfterEachAsync();
}