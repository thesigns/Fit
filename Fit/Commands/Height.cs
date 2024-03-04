namespace Fit.Commands;

public class Height : Command
{
    public new static string Manual => """
                                       Usage:
                                           fit height <height>
                                       Description:
                                           Stores current height.
                                           You must specify units. Only metric system is supported.
                                       Example:
                                           fit height 175cm
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
            Command.Execute("help height", repo);
            return "";
        }
        
        try
        {
            var weight = Units.GetMeasurement<Units.Mass>(args[0]);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            Command.Execute("help height", repo);
            return "";            
        }
        
        
        
        var height = Units.GetMeasurement<Units.Length>(args[0]);
        if (height < 0)
        {
            Command.Execute("help height", repo);
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
            var height = Units.GetMeasurement<Units.Length>(args[0]);
            fit.Height.Add((tick, height));
        }
        catch
        {
            throw new FormatException("Invalid measurement.");
        }
    }
}
