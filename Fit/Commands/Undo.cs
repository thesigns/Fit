namespace Fit.Commands;

public class Undo : Command
{
    private new static string Manual => """
                                        Usage:
                                            fit undo
                                        Description:
                                            Removes the last log record from the Fit Repository.
                                        Example:
                                            fit undo
                                        """;
    
    public override string Execute(List<string> args, Repo repo)
    {
        if (!repo.Exists())
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
        repo.UndoLog();
        return "";
    }
}
