using System.Globalization;
using Fit.Measures;

namespace Fit.Charts;

public class WeightChart : LinearChart
{
    public WeightChart(Fit fit, long fromTimeTick = long.MinValue, long toTimeTick = long.MaxValue)
    {
        ChartArea = new Rect(0, 0, new Size2D(1920, 1080));
        Padding = new Padding(200, 150, 150, 150);

        var name = fit.Name + (fit.Name.Last() == 's' ? "'" : "'s");
        
        Source += "\n<!-- Header -->\n";
        Source += $"<text x='{Padding.Left + PlotArea.Size.Width / 2}' y='{Padding.Top / 2}' class='title'>{name} Weight</text>\n";
        
        var minTimeTick = long.MaxValue;
        var maxTimeTick = long.MinValue;
        var minWeight = double.MaxValue;
        var maxWeight = double.MinValue;
        List<(double, double)> series = [];
        foreach (var weight in fit.Weights)
        {
            if (weight.tick < minTimeTick && weight.tick >= fromTimeTick)
            {
                minTimeTick = weight.tick;
            }
            if (weight.tick > maxTimeTick && weight.tick <= toTimeTick)
            {
                maxTimeTick = weight.tick;
            }
            var currentWeight = weight.weight.GetValue(Mass.Unit.Kilogram);
            if (currentWeight < minWeight)
            {
                minWeight = currentWeight;
            }
            if (currentWeight > maxWeight)
            {
                maxWeight = currentWeight;
            }
            double valueX = weight.tick;
            double valueY = weight.weight.GetValue(Mass.Unit.Kilogram);
            series.Add((valueX, valueY));
        }
        minTimeTick = new DateTime(minTimeTick).Date.Ticks;
        maxTimeTick = new DateTime(maxTimeTick).AddDays(1).Date.Ticks;
        var timeTicks = (new DateTime(maxTimeTick).Date - new DateTime(minTimeTick).Date).Days + 1;
        Source += GetXAxisSvg(minTimeTick, maxTimeTick, timeTicks, tickValue => new DateTime((long)tickValue).ToString("MM.dd"));
        
        minWeight = Math.Floor(minWeight) - 1;
        maxWeight = Math.Ceiling(maxWeight) + 1;
        var weightTicks = (int)(maxWeight - minWeight + 1);
        Source += GetYAxisSvg(minWeight, maxWeight, weightTicks, tickValue => tickValue.ToString(CultureInfo.InvariantCulture));

        Source += GetSeriesLineSvg(0, series, minTimeTick, maxTimeTick, minWeight, maxWeight);

    }
}