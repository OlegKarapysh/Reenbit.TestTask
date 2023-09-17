using Microsoft.Extensions.Azure;
using TestTask.WebAPI.Services.BlobStorageService;

namespace TestTask.WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAzureBlobClient(this IServiceCollection services, IConfiguration config)
    {
        services.AddAzureClients(b =>
        {
            b.AddBlobServiceClient(config.GetConnectionString("AzureBlobStorageConnectionString"));
        });
    }

    public static void AddBlobStorageService(this IServiceCollection services)
    {
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
}