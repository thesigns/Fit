using Fit.Repository;

namespace Fit.Commands;

public class Help : Command
{
    private new static string Manual => """
                                        Usage:
                                            fit help [<command>]
                                        Description:
                                            Displays help about a command or general help.
                                        """;
    
    public override string Execute(List<string> args, Repo repo)
    {
        if (args.Count == 0)
        {
            Console.WriteLine(Command.Manual);
            return "";
        }
        
        if (args.Count > 1)
        {
            Console.WriteLine(Help.Manual);
            return "";
        }
        
        switch (args[0])
        {
            case "help":
                Console.WriteLine(Help.Manual);
                break;
            case "init":
                Console.WriteLine(Init.Manual);
                break;
            case "height":
                Console.WriteLine(Height.Manual);
                break;
            case "weight":
                Console.WriteLine(Weight.Manual);
                break;
            case "emo":
                Console.WriteLine(Emo.Manual);
                break;
            case "undo":
                Console.WriteLine(Undo.Manual);
                break;
        }
        return "";
    }
    
    public override void ApplyToFit(long tick, string command, List<string> args, Fit fit)
    {
        
    }
}
