using Fit.Commands;

namespace Fit;

internal static class Program
{
    private static void Main(string[] args)
    {
        var repo = new Repo(Environment.CurrentDirectory, ".fit");
        Command.Execute(args, repo);
    }
}
