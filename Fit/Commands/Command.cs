namespace Fit.Commands;

public abstract class Command
{
    protected static string Manual => """
                                      Usage:
                                          fit <command> [<arguments>]
                                      Commands:
                                          help, init, height, weight, undo
                                      """;

    public abstract string Execute(List<string> args, Repo repo);
    
    public static void Execute(string[] args, Repo repo)
    {
        Execute(string.Join(' ', args), repo);
    }

    protected static void Execute(string argsString, Repo repo)
    {
        var args = argsString.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var command = args.Length > 0 ? args[0].ToLower() : "help";
        Execute(command, args.Skip(1).ToList(), repo);
    }

    private static void Execute(string command, List<string> args, Repo repo)
    {
        var logLine = "";
        switch (command)
        {
            case "help":
                logLine = new Help().Execute(args, repo);
                break;
            case "init":
                logLine = new Init().Execute(args, repo);
                break;
            case "height":
                logLine = new Height().Execute(args, repo);
                break;
            case "weight":
                logLine = new Weight().Execute(args, repo);
                break;
            case "undo":
                logLine = new Undo().Execute(args, repo);
                break;
        }
        repo.Log(logLine);
    }

    protected string ToCommandLine(IEnumerable<string> args)
    {
        return $"{GetType().Name.ToLower()} {string.Join(' ', args)}".Trim();
    }

}
