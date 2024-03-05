using Fit.Commands;
using Fit.Measures;

namespace Fit;

public class Fit
{
    public long Birth { get; set; }
    public Units.Sex Sex { get; set; }
    public List<(long tick, Length height)> Heights { get; set; } = [];
    public List<(long tick, Mass weight)> Weights { get; set; } = [];
    public List<(long tick, Mood mood)> Moods { get; set; } = [];


    public Fit(Repo repo)
    {
        var lines = repo.GetLog();
        foreach (var line in lines)
        {
            var (tick, commandLine) = Repo.SplitLogLine(line);
            Command.ApplyToFit(tick, commandLine, this);
        }
    }
   
    
}
