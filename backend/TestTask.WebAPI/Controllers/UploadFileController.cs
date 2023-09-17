using Microsoft.AspNetCore.Mvc;
using TestTask.WebAPI.Services.BlobStorageService;

namespace TestTask.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UploadFileController : ControllerBase
{
    private IBlobStorageService _blobStorageService;

    public UploadFileController(IBlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] string email)
    {
        try
        {
            await _blobStorageService.UploadFile(file, email);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}