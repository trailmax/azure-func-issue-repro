using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights()
    .AddScoped<MyScopedConfigurationService>()
    .AddScoped<MyApiService>()
    .AddHttpClient("MyApiClient", (services, httpClient) =>
    {
        var configService = services.GetRequiredService<MyScopedConfigurationService>();
        httpClient.BaseAddress = new Uri($"https://{configService.GetClientDomain}/");
    });


builder.Build().Run();


public class MyScopedConfigurationService
{
    public string GetClientDomain()
    {
        // do logic here based on other factors
        return "example.com";
    }
}


public class MyApiService
{
    private readonly HttpClient _httpClient;

    public MyApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("MyApiClient");
    }
}