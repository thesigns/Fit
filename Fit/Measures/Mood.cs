// namespace Fit.Measures;
//
// public class Mood
// {
//     public enum State
//     {
//         None,
//         Unknown,
//         ExtremelySad,
//         VerySad,
//         Sad,
//         Indifferent,
//         Happy,
//         VeryHappy,
//         ExtremelyHappy,
//         Angry,
//         Crying,
//         Tired,
//         Disappointed,
//         Focused,
//         Distracted,   
//         Confused,
//         Surprised,
//         Playful,
//         Laughing,
//         Affectionate,
//         InLove,
//     }
//
//     private static Dictionary<State, (string emoticon, int value)> Emotions { get; set; } = new()
//     {
//         { State.None, ("", 0)},
//         { State.Unknown, (":?", 0)},
//         { State.ExtremelySad, (":(((", -5)},
//         { State.VerySad, (":((", -4)},
//         { State.Sad, (":(", -2)},
//         { State.Indifferent, (":I",  0)},
//         { State.Happy, (":)",  2)},
//         { State.VeryHappy, (":))",  4)},
//         { State.ExtremelyHappy, (":)))",  5)},
//         { State.Angry, (">:[", -4)},
//         { State.Crying, (":'(",  -3)},
//         { State.Tired, ("X(", -2)},
//         { State.Disappointed, (":<",  -2)},
//         { State.Focused, ("*)",  2)},
//         { State.Distracted, ("@(",  -2)},
//         { State.Confused, (":S",  -1)},
//         { State.Surprised, (":O",  1)},
//         { State.Playful, (":P",  1)},
//         { State.Laughing, (":D",  2)},
//         { State.Affectionate, (":*",  4)},
//         { State.InLove, ("<3", 4)},
//     };
//
//     public State Value { get; }
//     public string Emoticon { get; }
//
//     public Mood(string notation)
//     {
//         Value = FindState(notation);
//         Emoticon = Emotions[Value].emoticon;
//     }
//
//     public static State FindState(string notation)
//     {
//         foreach(State mood in Enum.GetValues(typeof(State)))
//         {
//             if (notation == Emotions[mood].emoticon)
//             {
//                 return mood;
//             }
//         }
//         throw new FormatException("Uknown mood.");
//     }
//     
//     public override string ToString()
//     {
//         return Value.ToDelimitedString() + " " + Emoticon;
//     }
//     
// }