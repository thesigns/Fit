using System.Globalization;
using Fit.Measures;

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
            var fit = new Fit(repo);
            var previous = fit.Heights.Last();

            var height = new Length(args[0]);
            
            
            Console.WriteLine($"Previous height: {previous.height.Value(Length.Unit.cm)} cm ({Units.TicksToDate(previous.tick)})");
            var differenceValue = Math.Round(height.Value(Length.Unit.cm) - previous.height.Value(Length.Unit.cm), 3);
            var difference = differenceValue > 0 ? "+" + differenceValue : differenceValue.ToString(CultureInfo.CurrentCulture);
            Console.WriteLine($"New height: {height.Value(Length.Unit.cm)} cm [{difference} cm] ({Units.TicksToDate(DateTime.UtcNow.Ticks)})");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
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
            var height = new Length(args[0]);
            fit.Heights.Add((tick, height));
        }
        catch
        {
            throw new FormatException("Invalid measurement.");
        }
    }
}
