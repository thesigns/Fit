namespace Fit.Measures;

public class Length : Measure<Length, Length.Unit>, IMeasure<Length.Unit>
{
    public override string GetAbbreviation(Unit unit) => Abbreviations[unit];
    public override double GetBaseValue(Unit unit) => BaseValues[unit];
    public override Unit BaseUnit => Unit.Metre;
    
    public Length(string notation) : base(notation)
    {
    }
    
    public Length()
    {
    }

    public enum Unit
    {
        Mile,
        Kilometre,
        Metre,
        Yard,
        Foot,
        Inch,
        Decimetre,
        Centimetre,
        Millimetre
    }
    
    private static Dictionary<Unit, string> Abbreviations { get; set; } = new()
    {
        {Unit.Mile, "mi"},
        {Unit.Kilometre, "km"},
        {Unit.Metre,  "m"},
        {Unit.Yard, "yd"},
        {Unit.Foot, "ft"},
        {Unit.Inch, "in"},
        {Unit.Decimetre, "dm"},
        {Unit.Centimetre, "cm"},
        {Unit.Millimetre, "mm"},
    };

    private static Dictionary<Unit, double> BaseValues { get; set; } = new()
    {
        {Unit.Mile, 1609.344},
        {Unit.Kilometre, 1000},
        {Unit.Metre,  1},
        {Unit.Yard, 0.9144},
        {Unit.Foot, 0.3048},
        {Unit.Inch, 0.0254},
        {Unit.Decimetre, 0.1},
        {Unit.Centimetre, 0.01},
        {Unit.Millimetre, 0.001},
    };

}
