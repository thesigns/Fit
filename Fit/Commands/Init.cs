﻿using Fit.Attributes;
using Fit.Measures;
using Fit.Repository;

namespace Fit.Commands;

public class Init : Command
{
    public new static string Manual => """
                                       Usage:
                                           fit init <name> <date-of-birth> <sex> <height> <weight>
                                       Description:
                                           Initializes the Fit repository in the current directory.
                                           Name should be your name, a single word. Space isn't allowed.
                                           The date of birth should be in the YYYY-M-D format (leading zeroes are optional).
                                           Sex should be "male" or "m", "female" or "f", "intersex" or "i".
                                           Height and weight can be both in metric and imperial units.
                                       Example:
                                           fit init 1981.5.27 male 175cm 109kg
                                       """;
    
    public override string Execute(List<string> args, Repo repo)
    {
        if (args.Count < 5)
        {
            Command.Execute("help init", repo);
            return "";
        }

        try
        {
            var name = args[0];
            var birthTick = new Time(args[1]);
            var sex = new Sex(args[2]);
            var height = new Length(args[3]);
            var weight = new Mass(args[4]);
            var bmi = Bmi.GetValue(weight, height);
            Console.Write($"Initializing Fit repository for {name}, a ");
            Console.WriteLine($"{birthTick.YearsElapsed()} years old {sex}.");
            Console.WriteLine($"Height: {height.GetValue(Length.Unit.Centimetre)} cm");
            Console.WriteLine($"Weight: {weight.GetValue(Mass.Unit.Kilogram)} kg");
            Console.WriteLine($"BMI: {bmi} ({Bmi.GetDescription(bmi)})");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Command.Execute("help init", repo);
            return "";
        }
        try
        {
            repo.Create();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.ToString());
            return "";
        }
        Console.WriteLine("Fit repository initialized.");
        return ToCommandLine(args);
    }
    
    public override void ApplyToFit(long tick, string command, List<string> args, Fit fit)
    {
        if (args.Count < 4)
        {
            throw new ArgumentException("Too few args.");
        }
        try
        {
            var name = args[0];
            var birthTick = new Time(args[1]);
            var sex = new Sex(args[2]);
            var height = new Length(args[3]);
            var weight = new Mass(args[4]);
            fit.Name = name;
            fit.Birth = birthTick;
            fit.Sex = sex;
            fit.Heights.Add((tick, height));
            fit.Weights.Add((tick, weight));
        }
        catch
        {
            throw new FormatException("Invalid measurement.");
        }
    }
}
