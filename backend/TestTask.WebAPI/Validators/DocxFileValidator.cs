using FluentValidation;

namespace TestTask.WebAPI.Validators;

public sealed class DocxFileValidator : AbstractValidator<IFormFile>
{
    public const string AllowedExtension = ".docx";
    
    public DocxFileValidator()
    {
        RuleFor(f => f).NotNull().WithMessage("File is required!");
        RuleFor(f => f.FileName)
            .NotNull()
            .NotEmpty()
            .Must(x => Path.GetExtension(x) == AllowedExtension)
            .WithMessage($"Only {AllowedExtension} file extension is allowed!");
    }
}