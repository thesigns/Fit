using Fit.Repository;

namespace Fit.Commands;

public class Undo : Command
{
    private new static string Manual => """
                                        Usage:
                                            fit undo
                                        Description:
                                            Reverts (comments out) the last fit.log record in the Fit Repository.
                                        Example:
                                            fit undo
                                        """;
    
    public override string Execute(List<string> args, Repo repo)
    {
        if (!repo.Exists)
        {
            Console.WriteLine("Fit repository doesn't exist. Use init command.");
            Console.WriteLine();
            Command.Execute("help init", repo);
            return "";
        }
        
        if (args.Count > 0)
        {
            Command.Execute("help undo", repo);
            return "";
        }

        var log = new Log(repo.FitLogPath, Repo.Version);
        var reverted = log.Undo();
        if (!string.IsNullOrEmpty(reverted))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Reverted log: '{reverted}'");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        return "";
    }
    
    public override void ApplyToFit(long tick, string command, List<string> args, Fit fit)
    {
        
    }
}
