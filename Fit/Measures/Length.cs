namespace Fit.Measures;

public class Length(string notation) : Measure<Length, Length.Unit>(notation), IMeasure<Length.Unit>
{
    public new enum Unit
    {
        mm = 1000000,
        cm = 100000,
        dm = 10000,
        m = 1000,
        km = 1,
    }
    
    public Unit BaseUnit => Unit.m;
}
