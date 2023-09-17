namespace TestTask.WebAPI.Services.BlobStorageService;

public interface IBlobStorageService
{
    Task UploadFile(IFormFile file, string email);
}