namespace Fit.Commands;

public abstract class Command
{
    protected static string Manual => """
                                      Usage:
                                          fit <command> [<arguments>]
                                      Commands:
                                          help, init, height, weight, emo, undo
                                      """;

    public abstract string Execute(List<string> args, Repo repo);
    public abstract void ApplyToFit(long tick, string command, List<string> args, Fit fit);

    public static (string command, List<string> args) Split(string commandLine)
    {
        var splitCommandLine = commandLine.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var command = splitCommandLine.Length > 0 ? splitCommandLine[0].ToLower() : "help";
        var args = splitCommandLine.Skip(1).ToList();
        return (command, args);
    }
    
    public static void Execute(string[] args, Repo repo)
    {
        Execute(string.Join(' ', args), repo);
    }

    protected static void Execute(string commandLine, Repo repo)
    {
        var (command, args) = Split(commandLine);
        Execute(command, args, repo);
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
            case "emo":
                logLine = new Emo().Execute(args, repo);
                break;
            case "undo":
                logLine = new Undo().Execute(args, repo);
                break;

        }
        repo.Log(logLine);
    }
    
    public static void ApplyToFit(long tick, string commandLine, Fit fit)
    {
        var (command, args) = Split(commandLine);
        switch (command)
        {
            case "init":
                new Init().ApplyToFit(tick, command, args, fit);
                return;
            case "height":
                new Height().ApplyToFit(tick, command,args, fit);
                return;
            case "emo":
                new Emo().ApplyToFit(tick, command,args, fit);
                return;
            case "weight":
                new Weight().ApplyToFit(tick, command,args, fit);
                return;

        }
        throw new ArgumentException("Unsupported command line");
    }

    protected string ToCommandLine(IEnumerable<string> args)
    {
        return $"{GetType().Name.ToLower()} {string.Join(' ', args)}".Trim();
    }

}
