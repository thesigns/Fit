using System.Globalization;
using System.Text.RegularExpressions;

namespace Fit.Measures;

public partial class Measure<T, TUnitEnum> where T : Measure<T, TUnitEnum>, IMeasure<TUnitEnum> where TUnitEnum: struct, Enum
{
    [GeneratedRegex(@"^-?(\d*[.,]?\d*)([a-zA-Z]+)$")]
    private static partial Regex SplitMeasureRegex();

    private double BaseValue { get; }

    protected Measure(string notation)
    {
        var (valueInUnit, unit) = SplitMeasureNotation(notation);
        if (this is not T measure)
        {
            throw new Exception($"Not a {typeof(T).Name} measure.");
        }
        BaseValue = valueInUnit * Convert.ToInt32(measure.BaseUnit) / Convert.ToInt32(unit);
    }
    
    public double Value(TUnitEnum unit)
    {
        if (this is not T measure)
        {
            throw new Exception($"Not a {typeof(T).Name} measure.");
        }
        return BaseValue * Convert.ToInt32(unit) / Convert.ToInt32(measure.BaseUnit);
    }
    
    public double Value(string unitString)
    {
        if (this is not T measure)
        {
            throw new Exception($"Not a {typeof(T).Name} measure.");
        }
        if (Enum.TryParse<TUnitEnum>(unitString, true, out var unit))
        {
            return BaseValue * Convert.ToInt32(unit) / Convert.ToInt32(measure.BaseUnit);
        }
        throw new FormatException($"Error: {unitString} is not a {typeof(T).Name} unit.");
    }

    private static (double valueInUnit, TUnitEnum unit) SplitMeasureNotation(string notation)
    {
        var match = SplitMeasureRegex().Match(notation);
        if (!match.Success)
        {
            throw new FormatException($"Invalid {typeof(T).Name} measure format.");
        }
        var numericPart = match.Groups[1].Value.Replace(',', '.');
        var unitPart = match.Groups[2].Value;
        var valueInUnit = double.Parse(numericPart, CultureInfo.InvariantCulture);
        
        if (Enum.TryParse<TUnitEnum>(unitPart, true, out var unit))
        {
            return (valueInUnit, unit);
        }
        throw new FormatException($"Invalid {typeof(T).Name} measure format.");
    }
    
}