using Fit.Attributes;
using Fit.Commands;
using Fit.Measures;

namespace Fit;

internal static class Program
{
    private static void Main(string[] args)
    {
        var repo = new Repo(Environment.CurrentDirectory, ".fit");
        Command.Execute(args, repo);
    }
}
