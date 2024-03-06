namespace Fit.Attributes;

public class Sex : DescriptiveAttribute<Sex, Sex.State>
{
    
    public override string GetAbbreviation(State state) => States[state].abbreviation;
    public override int GetValue(State state) => States[state].value;
    
    public enum State
    {
        Male,
        Female,
        Intersex
    }

    private static Dictionary<State, (string abbreviation, int value)> States { get; set; } = new()
    {
        { State.Male, ("m", 0)},
        { State.Female, ("f", 1)},
        { State.Intersex, ("i", 2)},
    };
    
    public Sex(string notation) : base(notation)
    {
    }

}