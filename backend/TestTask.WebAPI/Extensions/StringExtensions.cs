using System.Text;

namespace TestTask.WebAPI.Extensions;

public static class StringExtensions
{
    public static string ParseRandomContainerName(this string source, int minLength, int maxLength)
    {
        if (minLength > maxLength || minLength < 0 || maxLength < 0)
        {
            throw new ArgumentException();
        }

        if (source.Length < minLength)
        {
            throw new Exception($"Source string must have at least {minLength} characters!");
        }
        
        var random = new Random();
        var strBuilder = new StringBuilder();
        for (int i = 0; i < source.Length; i++)
        {
            if (i >= maxLength)
            {
                break;
            }

            var symbol = source[i];
            var isValidSymbol = char.IsDigit(symbol) || char.IsLetter(symbol) || symbol == '-';
            if (isValidSymbol)
            {
                strBuilder.Append(symbol);
            }
            else
            {
                strBuilder.Append(GetRandomDigit());
            }
        }
        
        int GetRandomDigit() => random.Next(10);
        return strBuilder.ToString();
    }
}