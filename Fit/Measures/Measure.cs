using System.Globalization;
using System.Text.RegularExpressions;

namespace Fit.Measures;

public abstract partial class Measure<TDerived, TUnit>
    where TDerived : Measure<TDerived, TUnit>, new()
    where TUnit : struct, Enum
{
    private double Value { get; init; }

    protected Measure()
    {
    }

    protected Measure(string notation)
    {
        var parsedMeasures = ParseMeasure(notation);
        Value = Sum(parsedMeasures, BaseUnit);
    }
    
    [GeneratedRegex(@"(-?\d+([.,]\d+)?)([a-zA-Z]+)")]
    private static partial Regex MeasureRegex();
    
    private List<(double value, TUnit unit)> ParseMeasure(string notation)
    {
        var matches = MeasureRegex().Matches(notation);
        if (matches.Count == 0)
        {
            throw new FormatException($"Invalid {typeof(TUnit)} notation format.");
        }
        var valuesAndUnits = new List<(double value, TUnit unit)>();
        foreach (Match match in matches)
        {
            var valuePart = match.Groups[1].Value.Replace(',', '.');
            var unitPart = match.Groups[3].Value;
            var parsedValue = double.TryParse(valuePart, NumberStyles.Any, CultureInfo.InvariantCulture, out var value);
            var unit = GetUnit(unitPart);
            if (!parsedValue)
            {
                throw new FormatException($"Invalid {typeof(TUnit)} notation format.");
            }
            valuesAndUnits.Add((value, unit));
        }
        return valuesAndUnits;
    }

    private double Sum(List<(double value, TUnit unit)> valuesAndUnits, TUnit resultUnit)
    {
        var result = 0.0;
        foreach (var valueAndUnit in valuesAndUnits)
        {
            result += valueAndUnit.value * GetBaseValue(valueAndUnit.unit) /  GetBaseValue(resultUnit);
        }
        return result;
    }
    
    public double GetValue(TUnit unit, int roundTo = -1)
    {
        if (roundTo >= 0)
        {
            return Math.Round(Value * GetBaseValue(BaseUnit) / GetBaseValue(unit), roundTo);
        }
        return Value * GetBaseValue(BaseUnit) / GetBaseValue(unit);
    }
    
    public override string ToString()
    {
        return $"{Math.Round(Value, 3)} {BaseUnit}";
    }
    
    public static TDerived operator -(Measure<TDerived, TUnit> measure1, Measure<TDerived, TUnit> measure2)
    {
        var value = measure1.Value - measure2.Value;
        TDerived newValue = new()
        {
            Value = value
        };
        return newValue;
    }

    public TUnit GetUnit(string abbreviation)
    {
        foreach (TUnit enumValue in Enum.GetValues(typeof(TUnit)))
        {
            if (abbreviation == GetAbbreviation(enumValue))
            {
                return enumValue;
            }
        }
        throw new FormatException($"Invalid {typeof(TDerived)} unit abbreviation.");
    }
    
    public abstract string GetAbbreviation(TUnit unit);
    public abstract double GetBaseValue(TUnit unit);
    public abstract TUnit BaseUnit { get; }
}