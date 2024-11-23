namespace HomeInventory.IntegrationTest.Framework.Core;

public interface ISystemUnderTest : IAsyncDisposable
{
    IServiceProvider Services { get; }
    
    Task InitializeAsync();
}