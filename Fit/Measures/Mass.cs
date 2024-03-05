namespace Fit.Measures;

public class Mass : Measure<Mass, Mass.Unit>, IMeasure<Mass.Unit>
{
    public Mass(string notation) : base(notation)
    {
    }
    
    public Mass()
    {
    }

    public enum Unit
    {
        st,
        kg,
        dag,
        g,
        mg,
        lb,
        oz,
    }

    private static Dictionary<Unit, double> BaseValues { get; set; } = new()
    {
        {Unit.st, 6.35029318},
        {Unit.kg, 1},
        {Unit.lb, 0.45359237},
        {Unit.oz, 0.02834952312},
        {Unit.dag, 0.01},
        {Unit.g, 0.001},
        {Unit.mg, 0.000001},
    };
    
   public Unit BaseUnit => Unit.kg;
   public double GetBaseValue(Unit unit) => BaseValues[unit];
}
