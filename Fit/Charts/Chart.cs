using System.Globalization;

namespace Fit.Charts;

public class Chart
{
    public const string DefaultStyles = """
                                          @import url('https://fonts.googleapis.com/css2?family=Noto+Color+Emoji&amp;display=swap');

                                          .chartArea {
                                             fill: hsl(0, 0%, 10%);
                                          }

                                          .plotArea {
                                             fill: hsl(0, 0%, 15%);
                                          }

                                          .emoji {
                                             user-select: none;
                                             font-family: 'Noto Color Emoji';
                                             font-size: 20pt;
                                          }

                                          .middlemiddle {
                                             text-anchor: middle;
                                             dominant-baseline: middle;

                                          }

                                          .startmiddle {
                                             text-anchor: start;
                                             dominant-baseline: middle;
                                          }

                                          .endmiddle {
                                             text-anchor: end;
                                             dominant-baseline: middle;
                                          }

                                          .label {
                                             font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                             user-select: none;
                                             font-size: 12pt;
                                             fill: hsl(0, 0%, 60%);
                                          }

                                          .title {
                                             font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                             font-size: 30pt;
                                             font-weight: 100;
                                             text-anchor: middle;
                                             dominant-baseline: middle;
                                             fill: hsl(0, 0%, 60%);
                                          }

                                          .subtitle {
                                             font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                             font-size: 24pt;
                                             font-weight: 100;
                                             fill: #e2cb66;
                                          }

                                          .axis {
                                             stroke-width: 2;
                                             stroke: #555;
                                             fill: none;
                                          }

                                          .series0 {
                                             fill: rgb(255, 136, 0);
                                             stroke: rgb(255, 136, 0);
                                          }

                                          .series1 {
                                             fill: rgb(0, 183, 255);
                                             stroke: rgb(0, 183, 255);
                                          }

                                          .seriesline {
                                             stroke-width: 5;
                                             fill: none !important;
                                          }
                                          """;

    public Rect ChartArea { get; init; }
    public Padding Padding { get; init; }
    public Rect PlotArea
    {
       get
       {
          var plotAreaWidth = ChartArea.Size.Width - Padding.Left - Padding.Right;
          var plotAreaHeight = ChartArea.Size.Height - Padding.Top - Padding.Bottom;
          var plotAreaSize = new Size2D(plotAreaWidth, plotAreaHeight);
          return new Rect(ChartArea.XMin + Padding.Left, ChartArea.YMin + Padding.Top, plotAreaSize);
       }
    }

    public string Styles { get; set; } = DefaultStyles;
    public string Source { get; set; } = "";
   
    public void Save(string path)
    {
       var directoryPath = Path.GetDirectoryName(path);
       if (!Directory.Exists(directoryPath))
       {
          Directory.CreateDirectory(directoryPath);
       }
       var source = $"""
                      <svg
                      width="{ChartArea.Size.Width.ToString(CultureInfo.InvariantCulture)}" height="{ChartArea.Size.Height.ToString(CultureInfo.InvariantCulture)}" xmlns="http://www.w3.org/2000/svg">
                         <style>
                            {Styles}
                         </style>
                         <!-- Background -->
                         <rect x="0" y="0" width="100%" height="100%" class="chartArea"/>;
                         <rect x="{PlotArea.XMin.ToString(CultureInfo.InvariantCulture)}" y="{PlotArea.YMin.ToString(CultureInfo.InvariantCulture)}" width="{PlotArea.Size.Width.ToString(CultureInfo.InvariantCulture)}" height="{PlotArea.Size.Height.ToString(CultureInfo.InvariantCulture)}" class="plotArea"/>;
                         {Source}
                     </svg>
                     """;
         using (var streamWriter = new StreamWriter(path, false))
         {
           streamWriter.Write(source);
         }
    }
    
    public double GetValueXPosition(double value, double firstValue, double lastValue)
    {
       var pa = PlotArea;
       return pa.XMin + (value - firstValue) / (lastValue - firstValue) * (pa.XMax - pa.XMin);
    }
     
    public double GetValueYPosition(double value, double firstValue, double lastValue)
    {
       var pa = PlotArea;
       return pa.YMax + (value - firstValue) / (lastValue - firstValue) * (pa.YMin - pa.YMax);
    }
    
}