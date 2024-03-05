namespace Fit.Measures;

public interface IMeasure<TUnitEnum> where TUnitEnum : Enum
{
    TUnitEnum BaseUnit { get; }
    double GetBaseValue(TUnitEnum unit);
    //public static Dictionary<TUnitEnum, double> Amount => throw new NotImplementedException();
    
    
    //public static TUnitEnum BaseUnit => throw new NotImplementedException();


    //double ConvertToBaseUnit(double value, TUnitEnum unit);
}
