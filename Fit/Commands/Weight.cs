namespace Fit.Commands;

public class Weight : Command
{
    public new static string Manual => """
                                       Usage:
                                           fit weight <weight>
                                       Description:
                                           Stores current weight.
                                           You must specify units. Only metric system is supported.
                                       Example:
                                           fit weight 77.1kg
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
        
        if (args.Count != 1)
        {
            Command.Execute("help weight", repo);
            return "";
        }
        try
        {
            var weight = Units.GetMeasurement<Units.Mass>(args[0]);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            Command.Execute("help weight", repo);
            return "";            
        }
        return ToCommandLine(args);
    }
}
