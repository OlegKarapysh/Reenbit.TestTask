namespace TestTask.WebAPI.Services.BlobStorageService;

public interface IBlobStorageService
{
    Task<Uri> UploadFileAsync(IFormFile file, string email);
}