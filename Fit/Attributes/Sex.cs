namespace Fit.Attributes;

public class Sex : DescriptiveAttribute<Sex, Sex.State>
{
    public override string GetAbbreviation(State state) => States[state].abbreviation;
    public override string GetDescription(State state) => States[state].description;
    public override int GetValue(State state) => States[state].value;
    
    public enum State
    {
        Male,
        Female,
        Intersex
    }

    private static Dictionary<State, (string abbreviation, string description, int value)> States { get; set; } = new()
    {
        { State.Male, ("m", "Male", 0)},
        { State.Female, ("f", "Female", 1)},
        { State.Intersex, ("i", "Intersex", 2)},
    };
    
    public Sex(string notation) : base(notation)
    {
    }

}