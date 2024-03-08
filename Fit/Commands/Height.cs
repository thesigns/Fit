using System.Globalization;
using Fit.Measures;
using Fit.Repository;

namespace Fit.Commands;

public class Height : Command
{
    public new static string Manual => """
                                       Usage:
                                           fit height <height>
                                       Description:
                                           Stores current height.
                                           You must specify units. Both metric and imperial system is supported.
                                       Example:
                                           fit height 175cm
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
            
            
            Console.WriteLine($"Previous height: {previous.height.GetValue(Length.Unit.Centimetre)} cm ({new Time(previous.tick)})");
            var differenceValue = Math.Round(height.GetValue(Length.Unit.Centimetre) - previous.height.GetValue(Length.Unit.Centimetre), 3);
            var difference = differenceValue > 0 ? "+" + differenceValue : differenceValue.ToString(CultureInfo.CurrentCulture);
            Console.WriteLine($"New height: {Math.Round(height.GetValue(Length.Unit.Centimetre), 1)} cm [{difference} cm] ({new Time(DateTime.UtcNow.Ticks)})");
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
