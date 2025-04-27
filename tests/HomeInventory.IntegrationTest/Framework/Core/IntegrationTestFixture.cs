using Microsoft.Extensions.DependencyInjection;

namespace HomeInventory.IntegrationTest.Framework.Core;

public class IntegrationTestFixture<TEnvironment> : IIntegrationTestFixture
    where TEnvironment : IntegrationTestEnvironment
{
    private readonly TEnvironment _testEnvironment;
    private readonly List<IExternalDependency> _externalDependencies;
    private readonly ISystemUnderTest _systemUnderTest;

    private IServiceScope? _hostServiceScope;

    protected IntegrationTestFixture()
    {
        _testEnvironment = (TEnvironment)Activator.CreateInstance(typeof(TEnvironment), this)!;

        _externalDependencies = _testEnvironment.CreateExternalDependencies();
        _systemUnderTest = _testEnvironment.CreateSystemUnderTest();
    }

    public object? GetService(Type serviceType)
    {
        object? testService = _externalDependencies.SingleOrDefault(serviceType.IsInstanceOfType);
        if (testService is not null)
        {
            return testService;
        }

        if (serviceType.IsInstanceOfType(_systemUnderTest))
        {
            return _systemUnderTest;
        }
        
        return testService ?? _hostServiceScope!.ServiceProvider.GetService(serviceType);
    }

    public async Task InitializeAsync()
    {
        await Task.WhenAll(_externalDependencies.Select(testService => testService.InitializeAsync()));
        await _systemUnderTest.InitializeAsync();
    }

    public Task BeforeEachAsync()
    {
        _hostServiceScope = _systemUnderTest.Services.CreateScope();
        
        _testEnvironment.BeforeEachAsync();
        
        return Task.CompletedTask;
    }

    public Task AfterEachAsync()
    {
        _testEnvironment.AfterEachAsync();
        
        _hostServiceScope?.Dispose();
        _hostServiceScope = null;
        
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _systemUnderTest.DisposeAsync();
        await Task.WhenAll(_externalDependencies.Select(testService => testService.DisposeAsync()));
    }
}