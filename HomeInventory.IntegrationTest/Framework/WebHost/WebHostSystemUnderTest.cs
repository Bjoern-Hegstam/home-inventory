using HomeInventory.IntegrationTest.Framework.Core;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HomeInventory.IntegrationTest.Framework.WebHost;

public class WebHostSystemUnderTest<TProgram> : WebApplicationFactory<TProgram>, ISystemUnderTest
    where TProgram : class
{
    private HttpClient? _httpClient;

    public HttpClient HttpClient => _httpClient!;
    
    public Task InitializeAsync()
    {
        _httpClient = CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost");
        return Task.CompletedTask;
    }
}