namespace Fit.Attributes;

public class Mood : DescriptiveAttribute<Mood, Mood.State>
{
    public override string GetAbbreviation(State state) => States[state].abbreviation;
    public override string GetDescription(State state) => States[state].description;
    public override int GetValue(State state) => States[state].value;
    
    public enum State
    {
        None,
        ExtremelySad,
        VerySad,
        Sad,
        Indifferent,
        Happy,
        VeryHappy,
        ExtremelyHappy,
        Angry,
        Crying,
        Tired,
        Disappointed,
        Focused,
        Distracted,   
        Confused,
        Surprised,
        Playful,
        Partying,
        Laughing,
        Affectionate,
        InLove,
        Indescribable,
    }

    private static Dictionary<State, (string abbreviation, string description, int value)> States { get; set; } = new()
    {
        { State.None, ("", "", 0)},
        { State.ExtremelySad, (":(((", "\ud83d\ude2d", -5)},
        { State.VerySad, (":((", "\u2639\ufe0f", -4)},
        { State.Sad, (":(", "\ud83d\ude41", -2)},
        { State.Indifferent, (":I", "\ud83d\ude10",  0)},
        { State.Happy, (":)", "\ud83d\ude42",  2)},
        { State.VeryHappy, (":))", "\ud83d\ude00",  4)},
        { State.ExtremelyHappy, (":)))", "\ud83d\ude02",  5)},
        { State.Angry, (">:[", "\ud83d\ude21", -4)},
        { State.Crying, (":'(", "\ud83d\ude22",  -3)},
        { State.Tired, ("X(", "\ud83d\ude2b", -2)},
        { State.Disappointed, (":<", "\ud83d\ude12",  -2)},
        { State.Focused, ("*)", "\ud83e\uddd0",  2)},
        { State.Distracted, ("@(", "\ud83d\ude35",  -2)},
        { State.Confused, (":S", "\ud83d\ude15",  -1)},
        { State.Surprised, (":O", "\ud83d\ude2e",  1)},
        { State.Playful, (";P", "\ud83d\ude1c",  1)},
        { State.Partying, ("<:)", "\ud83e\udd73",  2)},
        { State.Laughing, (":D", "\ud83d\ude06",  2)},
        { State.Affectionate, (":*", "\ud83d\ude17",  4)},
        { State.InLove, ("<3", "\ud83e\udd70", 4)},
        { State.Indescribable, (":?", "\ud83e\udee5", 0)},
    };

    public Mood(string notation) : base(notation)
    {
    }
    
    public override string ToString()
    {
        return $"{Value.ToDelimitedString()} {States[Value].abbreviation}";
    }

}