using Fit.Commands;

namespace Fit;

public class Fit
{
    public long Birth { get; set; }
    public Units.Sex Sex { get; set; }
    public List<(long tick, double height)> Height { get; set; } = [];
    public List<(long tick, double weight)> Weight { get; set; } = [];


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
