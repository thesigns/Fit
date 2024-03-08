using Fit.Commands;
using Fit.Repository;

namespace Fit;

internal static class Program
{
    private static void Main(string[] args)
    {
        var repo = new Repo(Environment.CurrentDirectory);
        Command.Execute(args, repo);
    }
}
