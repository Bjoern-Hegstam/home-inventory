using Xunit;

namespace HomeInventory.IntegrationTest.Framework.Core;

public abstract class IntegrationTestBase(IIntegrationTestFixture fixture) : IAsyncLifetime
{
    protected IIntegrationTestFixture Fixture { get; } = fixture;
    
    public async Task InitializeAsync()
    {
        await Fixture.BeforeEachAsync();
    }

    public async Task DisposeAsync()
    {
        await Fixture.AfterEachAsync();
    }
}