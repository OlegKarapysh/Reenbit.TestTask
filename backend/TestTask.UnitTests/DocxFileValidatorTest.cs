using FluentValidation;
using Microsoft.AspNetCore.Http;
using Moq;
using TestTask.WebAPI.Validators;

namespace TestTask.UnitTests;

public class DocxFileValidatorTest
{
    private readonly AbstractValidator<IFormFile> _sut;
    private readonly Mock<IFormFile> _fileMock;

    public DocxFileValidatorTest()
    {
        _sut = new DocxFileValidator();
        _fileMock = new Mock<IFormFile>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("s.txt")]
    [InlineData("s.")]
    [InlineData("s")]
    public void DocxFileValidator_ShouldReturnNotValidValidationResult_WhenFileNameIsInvalid(string fileName)
    {
        // Assign.
        _fileMock.Setup(x => x.FileName).Returns(fileName);
        
        // Act.
        var result = _sut.Validate(_fileMock.Object);
        
        // Assert.
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void DocxFileValidator_ShouldReturnValidValidationResult_WhenFileNameIsValid()
    {
        // Assign.
        _fileMock.Setup(x => x.FileName).Returns("filename.docx");
        
        // Act.
        var result = _sut.Validate(_fileMock.Object);
        
        // Assert.
        result.IsValid.Should().BeTrue();
    }
}