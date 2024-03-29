﻿using System.Globalization;
using Fit.Measures;
using Fit.Repository;

namespace Fit.Commands;

public class Weight : Command
{
    public new static string Manual => """
                                       Usage:
                                           fit weight <weight>
                                       Description:
                                           Stores current weight.
                                           You must specify units. Both metric and imperial system is supported.
                                       Example:
                                           fit weight 77.1kg
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
            Command.Execute("help weight", repo);
            return "";
        }
        try
        {
            var fit = new Fit(repo);
            var previous = fit.Weights.Last();

            var weight = new Mass(args[0]);
            
            
            Console.WriteLine($"Previous weight: {previous.weight.GetValue(Mass.Unit.Kilogram)} kg ({new Time(previous.tick)})");
            var differenceValue = Math.Round(weight.GetValue(Mass.Unit.Kilogram) - previous.weight.GetValue(Mass.Unit.Kilogram), 3);
            var difference = differenceValue > 0 ? "+" + differenceValue : differenceValue.ToString(CultureInfo.CurrentCulture);
            Console.WriteLine($"New weight: {Math.Round(weight.GetValue(Mass.Unit.Kilogram), 1)} kg [{difference} kg] ({new Time(DateTime.UtcNow.Ticks)})");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            Command.Execute("help weight", repo);
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
            var weight = new Mass(args[0]);
            fit.Weights.Add((tick, weight));
        }
        catch
        {
            throw new FormatException("Invalid measurement.");
        }
    }
}
