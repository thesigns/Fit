using System.Globalization;

namespace Fit.Charts;

public class LinearChart : Chart
{
    public string GetXAxisSvg(double fromValue, double toValue, int ticks, Func<double, string> valueToString)
    {
        var result = "<!-- X Axis -->";
        var stepSize = (toValue - fromValue) / (ticks - 1);
        for (var i = 0; i < ticks; i++)
        {
            var tickValue = valueToString(fromValue + stepSize * i);
            var x = (PlotArea.XMin + PlotArea.Size.Width / (ticks - 1) * i).ToString(CultureInfo.InvariantCulture);
            result += $"<line x1='{x}' y1='{PlotArea.YMax - 5}' x2='{x}' y2='{PlotArea.YMax + 5}' class='axis'/>\n";                
            result += $"<text x='{x}' y='{PlotArea.YMax + 25}' class='middlemiddle label'>{tickValue}</text>\n";
        }
        return result;
    }
    
    public string GetYAxisSvg(double fromValue, double toValue, int ticks, Func<double, string> valueToString)
    {
        var result = "<!-- Y Axis -->";
        var stepSize = (toValue - fromValue) / (ticks - 1);
        for (var i = 0; i < ticks; i++)
        {
            var tickValue = valueToString(fromValue + stepSize * i);
            var y = (PlotArea.YMax - PlotArea.Size.Height / (ticks - 1) * i).ToString(CultureInfo.InvariantCulture);
            result += $"<line x1='{PlotArea.XMin - 5}' y1='{y}' x2='{PlotArea.XMin + 5}' y2='{y}' class='axis'/>\n";                
            result += $"<text x='{PlotArea.XMin - 15}' y='{y}' class='endmiddle label'>{tickValue}</text>\n";
        }
        return result;
    }

    public string GetSeriesLineSvg(int seriesIndex, List<(double x, double y)> series, double xMin, double xMax, double yMin, double yMax)
    {

        var points = "";
        
        foreach (var value in series)
        {
            var x = GetValueXPosition(value.x, xMin, xMax);
            var y = GetValueYPosition(value.y, yMin, yMax);
            points += x.ToString(CultureInfo.InvariantCulture) + "," + y.ToString(CultureInfo.InvariantCulture) + " ";
        }
        return $"<polyline points='{points}' class='seriesline series{seriesIndex}'/>\n";
    }
    
}