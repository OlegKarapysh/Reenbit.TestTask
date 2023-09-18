using TestTask.WebAPI.Extensions;

namespace TestTask.UnitTests;

public class StringExtensionsTest
{
    private const int MaxContainerNameLength = 63;
    private const int MinContainerNameLenght = 3;

    [Theory]
    [InlineData("test@gmail.com")]
    [InlineData("anothertest@i.ua")]
    [InlineData("a@a.a")]
    [InlineData("someverylongemailsomeverylongemailsomeverylongemailsomeverylongemailsomevegel@gmail.com")]
    public void ParseRandomContainerName_ShouldReturnValidContainerName_IfArgumentsAreValid(string email)
    {
        // Act.
        var sut = email.ParseRandomContainerName(MinContainerNameLenght, MaxContainerNameLength);
        
        // Assert.
        sut.Length.Should().BeLessOrEqualTo(MaxContainerNameLength).And.BeGreaterOrEqualTo(MinContainerNameLenght);
        sut.All(IsValidCharacter).Should().BeTrue();
    }

    [Theory]
    [InlineData(5, 1)]
    [InlineData(-1, 5)]
    [InlineData(5, -1)]
    [InlineData(-1, -1)]
    [InlineData(100, 120)]
    public void ParseRandomContainerName_ShouldThrowException_IfArgumentsAreInvalid(int minLength, int maxLength)
    {
        // Assign.
        var email = "something@i.ua";
        
        // Act.
        var tryParseContainerName = () =>
        {
            email.ParseRandomContainerName(minLength, maxLength);
        };
        
        // Assert.
        tryParseContainerName.Should().Throw<Exception>();
    }

    private bool IsValidCharacter(char character) => 
        char.IsLetter(character) || char.IsDigit(character) || character == '-';
}