using System.Globalization;
using System.Text.RegularExpressions;

namespace Fit.Measures;

public partial class Measure<T, TUnitEnum> where T : Measure<T, TUnitEnum>, IMeasure<TUnitEnum>, new() where TUnitEnum: struct, Enum
{
    private double Value { get; init; }

    protected Measure()
    {
    }

    protected Measure(string notation)
    {
        if (this is not T measure)
        {
            throw new Exception($"Object of type {typeof(T).Name} is not a Measure.");
        }
        var parsedMeasures = ParseMeasure(notation);
        Value = Sum(parsedMeasures, measure.BaseUnit);
    }

    [GeneratedRegex(@"(-?\d+([.,]\d+)?)([a-zA-Z]+)")]
    private static partial Regex MeasureRegex();
    
    private static List<(double value, TUnitEnum unit)> ParseMeasure(string notation)
    {
        var matches = MeasureRegex().Matches(notation);
        if (matches.Count == 0)
        {
            throw new FormatException($"Invalid {typeof(T).Name} measure format.");
        }
        var valuesUnits = new List<(double value, TUnitEnum unit)>();
        foreach (Match match in matches)
        {
            var valuePart = match.Groups[1].Value.Replace(',', '.');
            var unitPart = match.Groups[3].Value;
            var parsedValue = double.TryParse(valuePart, NumberStyles.Any, CultureInfo.InvariantCulture, out var value);
            var parsedUnit = Enum.TryParse<TUnitEnum>(unitPart, true, out var unit);
            if (!parsedValue || !parsedUnit)
            {
                throw new FormatException($"Invalid {typeof(T).Name} measure format.");
            }
            valuesUnits.Add((value, unit));
        }
        return valuesUnits;
    }

    private double Sum(List<(double value, TUnitEnum unit)> valuesUnits, TUnitEnum resultUnit)
    {
        var result = 0.0;
        if (this is not T measure)
        {
            throw new Exception($"Object of type {typeof(T).Name} is not a Measure.");
        }
        foreach (var vu in valuesUnits)
        {
            result += vu.value * measure.GetBaseValue(vu.unit) /  measure.GetBaseValue(resultUnit);
        }
        return result;
    }
    
    public double GetValue(TUnitEnum unit)
    {
        if (this is not T measure)
        {
            throw new Exception($"Object of type {typeof(T).Name} is not a Measure.");
        }
        return Value * measure.GetBaseValue(measure.BaseUnit) / measure.GetBaseValue(unit);
    }
    
    public override string ToString()
    {
        if (this is not T measure)
        {
            throw new Exception($"Object of type {typeof(T).Name} is not a Measure.");
        }
        return $"{Math.Round(Value, 3)} {measure.BaseUnit}";
    }
    
    public static T operator -(Measure<T, TUnitEnum> measure1, Measure<T, TUnitEnum> measure2)
    {
        var value = measure1.Value - measure2.Value;
        T newValue = new()
        {
            Value = value
        };
        return newValue;
    }

}