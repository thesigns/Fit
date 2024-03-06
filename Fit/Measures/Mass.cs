namespace Fit.Measures;

public class Mass : Measure<Mass, Mass.Unit>
{
    public override string GetAbbreviation(Unit unit) => Abbreviations[unit];
    public override double GetBaseValue(Unit unit) => BaseValues[unit];
    public override Unit BaseUnit => Unit.Kilogram;
    
    public Mass(string notation) : base(notation)
    {
    }
    
    public Mass()
    {
    }
    
    public enum Unit
    {
        Stone,
        Kilogram,
        Pound,
        Ounce,
        Decagram,
        Gram,
        Milligram,
        Microgram,
    }
    
    private static Dictionary<Unit, string> Abbreviations { get; set; } = new()
    {
        {Unit.Stone, "st"},
        {Unit.Kilogram, "kg"},
        {Unit.Pound,  "lb"},
        {Unit.Ounce, "oz"},
        {Unit.Decagram, "dag"},
        {Unit.Gram, "g"},
        {Unit.Milligram, "mg"},
        {Unit.Microgram, "mcg"},
    };

    private static Dictionary<Unit, double> BaseValues { get; set; } = new()
    {
        {Unit.Stone, 6350.29318},
        {Unit.Kilogram, 1000},
        {Unit.Pound,  453.59237},
        {Unit.Ounce, 28.34952312},
        {Unit.Decagram, 10},
        {Unit.Gram, 1},
        {Unit.Milligram, 0.001},
        {Unit.Microgram, 0.000001},
    };

}
