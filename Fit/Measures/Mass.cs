namespace Fit.Measures;

public class Mass(string notation) : Measure<Mass, Mass.Unit>(notation), IMeasure<Mass.Unit>
{
    public enum Unit
    {
        mg = 1000000,
        g = 1000,
        dag = 100,
        kg = 1,
    }
    
    public Unit BaseUnit => Unit.kg;
}
