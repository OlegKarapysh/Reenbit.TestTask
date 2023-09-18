using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using TestTask.WebAPI.Extensions;

namespace TestTask.WebAPI.Services.BlobStorageService;

public sealed class BlobStorageService : IBlobStorageService
{
    private const int MaxContainerNameLength = 63;
    private const int MinContainerNameLenght = 3;
    private BlobServiceClient _blobClient;

    public BlobStorageService(BlobServiceClient blobClient)
    {
        _blobClient = blobClient;
    }

    public async Task<Uri> UploadFileAsync(IFormFile file, string email)
    {
        var containerClient = _blobClient.GetBlobContainerClient(
                email.ParseRandomContainerName(MinContainerNameLenght, MaxContainerNameLength));
        await containerClient.CreateIfNotExistsAsync();
        var blobClient = containerClient.GetBlobClient(file.FileName);
        await using var fileStream = file.OpenReadStream();
        await blobClient.UploadAsync(fileStream);
        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = containerClient.Name,
            BlobName = blobClient.Name,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
        };
        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        return blobClient.GenerateSasUri(sasBuilder);
    }
}