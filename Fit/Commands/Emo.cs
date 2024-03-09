using Fit.Attributes;
using Fit.Measures;
using Fit.Repository;

namespace Fit.Commands;

public class Emo : Command
{
        public new static string Manual => """
                                           Usage:
                                               fit emo <emoticon>
                                           Description:
                                               Stores current mood/emotion using emoticons as input.
                                           Allowed emoticons:
                                               :((( - Extremely Sad
                                               :((  - Very Sad
                                               :(   - Sad
                                               :I   - Indifferent
                                               :)   - Happy
                                               :))  - Very Happy
                                               :))) - Extremely Happy
                                               >:[  - Angry
                                               :'(  - Crying
                                               X(   - Tired
                                               :<   - Disappointed
                                               *)   - Focused
                                               @(   - Distracted   
                                               :S   - Confused
                                               :O   - Surprised
                                               ;P   - Playful
                                               :D   - Laughing
                                               :*   - Affectionate
                                               <3   - In Love
                                               :?   - Indescribable
                                           Example:
                                               fit emo :P
                                           """;
    
    public override string Execute(List<string> args, Repo repo)
    {

        
        if (!repo.Exists)
        {
            Console.WriteLine("Fit repository doesn't exist. Use init command.");
            return "";
        }
        
        if (args.Count != 1)
        {
            Command.Execute("help emo", repo);
            return "";
        }

        try
        {
            var fit = new Fit(repo);
            var previous = fit.Moods.Count > 0 ? fit.Moods.Last().mood : null;
            if (previous != null)
            {
                Console.WriteLine($"Previous mood: {previous} ({new Time(fit.Moods.Last().tick)})");                
            }
            var currentMood = new Mood(args[0]);
            Console.WriteLine($"Current mood: {currentMood} ({new Time(DateTime.UtcNow.Ticks)})");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            Command.Execute("help emo", repo);
            return "";            
        }
        return ToCommandLine(args);
    }

    public override void ApplyToFit(long tick, string command, List<string> args, Fit fit)
    {
        if (args.Count != 1)
        {
            throw new ArgumentException("Just one arg required.");
        }
        try
        {
            var mood = new Mood(args[0]);
            fit.Moods.Add((tick, mood));
        }
        catch
        {
            throw new FormatException("Invalid mood.");
        }
    }
}