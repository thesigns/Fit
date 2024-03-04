namespace Fit.Measures;

public interface IMeasure<out TUnitEnum>
{
    public TUnitEnum BaseUnit { get; }
}