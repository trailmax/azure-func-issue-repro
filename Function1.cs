using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionScopingIssue;

public class Function1
{
    private readonly ILogger<Function1> _logger;
    private readonly MyApiService _myApiService;

    public Function1(ILogger<Function1> logger, MyApiService myApiService)
    {
        _logger = logger;
        _myApiService = myApiService;
    }

    [Function("Function1")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}