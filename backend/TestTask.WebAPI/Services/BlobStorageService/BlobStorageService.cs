using Azure.Storage.Blobs;
using TestTask.WebAPI.Extensions;

namespace TestTask.WebAPI.Services.BlobStorageService;

public sealed class BlobStorageService : IBlobStorageService
{
    private const int MaxContainerNameLength = 63;
    private const int MinContainerNameLenght = 3;
    private const string AllowedExtension = ".docx";
    private BlobServiceClient _blobClient;

    public BlobStorageService(BlobServiceClient blobClient)
    {
        _blobClient = blobClient;
    }

    public async Task UploadFile(IFormFile file, string email)
    {
        if (Path.GetExtension(file.FileName) != AllowedExtension)
        {
            throw new ArgumentException($"Only {AllowedExtension} file extension is allowed!");
        }
        
        var containerClient = _blobClient.GetBlobContainerClient(
                email.ParseRandomContainerName(MinContainerNameLenght, MaxContainerNameLength));
        await containerClient.CreateIfNotExistsAsync();
        var blobClient = containerClient.GetBlobClient(file.FileName);
        await using var fileStream = file.OpenReadStream();
        await blobClient.UploadAsync(fileStream);

        Console.WriteLine("Uri: " + blobClient.Uri);
    }
}