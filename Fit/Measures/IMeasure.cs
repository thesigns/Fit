namespace Fit.Measures;

public interface IMeasure<TUnit>
{
    public string GetAbbreviation(TUnit unit);
    double GetBaseValue(TUnit unit);
    TUnit? BaseUnit { get; }
}
