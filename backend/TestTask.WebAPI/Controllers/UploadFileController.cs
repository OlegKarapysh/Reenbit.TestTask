using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TestTask.WebAPI.Services.BlobStorageService;

namespace TestTask.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UploadFileController : ControllerBase
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly IValidator<IFormFile> _docxFileValidator;
    private readonly IValidator<string> _emailValidator;

    public UploadFileController(IBlobStorageService blobStorageService, IValidator<IFormFile> docxFileValidator, IValidator<string> emailValidator)
    {
        _blobStorageService = blobStorageService;
        _docxFileValidator = docxFileValidator;
        _emailValidator = emailValidator;
    }

    [HttpPost]
    public async Task<ActionResult<Uri>> UploadFile([FromForm] IFormFile file, [FromForm] string email)
    {
        var emailValidation = await _emailValidator.ValidateAsync(email);
        if (!emailValidation.IsValid)
        {
            return BadRequest(emailValidation.ToString());
        }

        var fileValidation = await _docxFileValidator.ValidateAsync(file);
        if (!fileValidation.IsValid)
        {
            return BadRequest(fileValidation.ToString());
        }
        
        return Ok(await _blobStorageService.UploadFileAsync(file, email));
    }
}