namespace Fit.Attributes;

public abstract class DescriptiveAttribute<TDerived, TState>
    where TDerived : DescriptiveAttribute<TDerived, TState>
    where TState : struct, Enum
{
    
    public abstract string GetAbbreviation(TState unit);
    public abstract int GetValue(TState unit);
    
    public TState Value { get; set; }
    
    protected DescriptiveAttribute(string notation)
    {
        Value = Parse(notation);
    }

    private TState Parse(string notation)
    {
        foreach (var state in Enum.GetValues<TState>())
        {
            if (notation.Equals(state.ToString(), StringComparison.CurrentCultureIgnoreCase) || notation == GetAbbreviation(state))
            {
                return state;
            }
        }
        throw new FormatException($"Invalid {typeof(TDerived).Name} type.");
    }

    public override string ToString()
    {
        return $"{Value.ToString()}";
    }
    
}