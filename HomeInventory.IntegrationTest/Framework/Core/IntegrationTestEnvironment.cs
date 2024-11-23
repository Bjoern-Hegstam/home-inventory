namespace HomeInventory.IntegrationTest.Framework.Core;

public abstract class IntegrationTestEnvironment(IServiceProvider testServices)
{
    protected IServiceProvider TestServices { get; } = testServices;

    public virtual List<IExternalDependency> CreateExternalDependencies() => [];
    
    public abstract ISystemUnderTest CreateSystemUnderTest();
    
    public virtual Task BeforeEachAsync() => Task.CompletedTask;
    public virtual Task AfterEachAsync() => Task.CompletedTask;                      
}