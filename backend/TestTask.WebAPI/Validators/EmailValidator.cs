using FluentValidation;

namespace TestTask.WebAPI.Validators;

public sealed class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(s => s).NotNull().NotEmpty().WithMessage("Email address is required!")
                       .MinimumLength(5).WithMessage("Email address should have at least 5 characters!")
                       .MaximumLength(75).WithMessage("Email address should have at most 75 characters!")
                       .EmailAddress().WithMessage("This is not a valid email address format!");
    }
}