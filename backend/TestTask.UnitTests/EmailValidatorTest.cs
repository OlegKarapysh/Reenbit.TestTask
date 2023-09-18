using FluentValidation;
using TestTask.WebAPI.Validators;

namespace TestTask.UnitTests;

public class EmailValidatorTest
{
    private readonly AbstractValidator<string> _sut;

    public EmailValidatorTest()
    {
        _sut = new EmailValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData("a@a.")]
    [InlineData("verylongemailverylongemailverylongemailverylongemailverylongemailverylongemailvery@gmail.com")]
    public void EmailValidator_ShouldReturnNotValidValidationResult_WhenEmailHasWrongLength(string email)
    {
        // Act.
        var result = _sut.Validate(email);
        
        // Assert.
        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("aaaaa")]
    [InlineData("@%(@%#")]
    [InlineData(".......")]
    public void EmailValidator_ShouldReturnNotValidValidationResult_WhenEmailHasInvalidFormat(string email)
    {
        // Act.
        var result = _sut.Validate(email);
        
        // Assert.
        result.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("something@gmail.com")]
    [InlineData("email@i.ua")]
    public void EmailValidator_ShouldReturnValidValidationResult_WhenEmailIsValid(string email)
    {
        // Act.
        var result = _sut.Validate(email);
        
        // Assert.
        result.IsValid.Should().BeTrue();
    }
}