namespace Fit.Measures;

public class Length : Measure<Length, Length.Unit>, IMeasure<Length.Unit>
{
    public Length(string notation) : base(notation)
    {
    }
    
    public Length()
    {
    }

    public enum Unit
    {
        mi,
        km,
        m,
        yd,
        ft,
        In,
        dm,
        cm,
        mm
    }

    private static Dictionary<Unit, double> BaseValues { get; set; } = new()
    {
        {Unit.mi, 1609.344},
        {Unit.km, 1000},
        {Unit.m,  1},
        {Unit.yd, 0.9144},
        {Unit.ft, 0.3048},
        {Unit.In, 0.0254},
        {Unit.dm, 0.1},
        {Unit.cm, 0.01},
        {Unit.mm, 0.001},
    };
    
    public Unit BaseUnit => Unit.m;
    public double GetBaseValue(Unit unit) => BaseValues[unit];
}
