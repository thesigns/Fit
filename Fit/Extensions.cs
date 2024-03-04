using System.Text.RegularExpressions;

namespace Fit;

public static class Extensions
{
    
    /// <summary>
    /// Converts an enum value to a human-readable string with spaces between words,
    /// assuming the enum is in PascalCase.
    /// For example, 'VeryHappySmile' becomes "Very Happy Smile".
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="value">The enum value to convert.</param>
    /// <returns>A human-readable string representing the enum value.</returns>
    public static string ToDelimitedString<TEnum>(this TEnum value) where TEnum : Enum
    {
        return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1 $2");
    }
    
}