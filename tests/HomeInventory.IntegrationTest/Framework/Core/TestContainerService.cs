using DotNet.Testcontainers.Containers;

namespace HomeInventory.IntegrationTest.Framework.Core;

public abstract class TestContainerService<TContainer> : IExternalDependency
    where TContainer : IContainer
{
    private readonly Lazy<TContainer> _container;

    protected TContainer Container => _container.Value;

    protected TestContainerService()
    {
        _container = new Lazy<TContainer>(CreateContainer);
    }

    protected abstract TContainer CreateContainer();

    public async Task InitializeAsync()
    {
        await _container.Value.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.Value.StopAsync();
    }
}