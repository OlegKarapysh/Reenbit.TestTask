using System.Text;
using System.Text.Json;
using TestTask.WebAPI.DTO;

namespace TestTask.WebAPI.Services.AzureFunctionTriggerService;

public sealed class AzureFunctionTriggerService : IAzureFunctionTriggerService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AzureFunctionTriggerService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task CallEmailNotificationFunction(Uri fileUri, string email)
    {
        using var httpClient = _httpClientFactory.CreateClient("AzureFunctionHttpClient");
        var payload = new FileEmailDto
        {
            Email = email,
            FileUri = fileUri.ToString()
        };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("", content);
        Console.WriteLine(response.ToString());
    }
}