namespace Fit.Commands;

public class Mood : Command
{
        public new static string Manual => """
                                       Usage:
                                           fit mood <emoticon>
                                       Description:
                                           Stores current mood/emotion using emoticons.
                                       Allowed emoticons:
                                           :((( - Extremely Sad
                                           :((  - Very Sad
                                           :(   - Sad
                                           :|   - Indifferent
                                           :)   - Happy
                                           :))  - Very Happy
                                           :))) - Extremely Happy
                                           >:[  - Angry
                                           :'(  - Crying
                                           X(   - Tired   
                                           :<   - Disappointed
                                           :S   - Confused
                                           :O   - Surprised
                                           :P   - Playful
                                           :*   - Affectionate
                                           <3   - In Love
                                       Example:
                                           fit mood :P
                                       """;
    
    public override string Execute(List<string> args, Repo repo)
    {
        if (!repo.Exists())
        {
            Console.WriteLine("Fit repository doesn't exist. Use init command.");
            return "";
        }
        
        if (args.Count != 1)
        {
            Command.Execute("help mood", repo);
            return "";
        }
        try
        {
            var fit = new Fit(repo);
            var previous = fit.Moods.Count > 0 ? fit.Moods.Last().mood : Units.Mood.None;
            if (previous != Units.Mood.None)
            {
                var previousMood = $"{previous.ToDelimitedString()} {Units.GetMoodEmoticon(previous)}";
                Console.WriteLine($"Previous mood: {previousMood} ({Units.TicksToDate(fit.Moods.Last().tick)})");                
            }
            var currentMood = $"{Units.GetMood(args[0]).ToDelimitedString()} {args[0]}";
            Console.WriteLine($"Current mood: {currentMood} ({Units.TicksToDate(DateTime.UtcNow.Ticks)})");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            Command.Execute("help mood", repo);
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
            var mood = Units.GetMood(args[0]);
            fit.Moods.Add((tick, mood));
        }
        catch
        {
            throw new FormatException("Invalid measurement.");
        }
    }
}