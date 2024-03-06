using System.Text.RegularExpressions;

namespace Fit;

public static class Extensions
{
    
    public static string ToDelimitedString<TEnum>(this TEnum value) where TEnum : Enum
    {
        return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1 $2");
    }
    
}