namespace Fit.Charts;

public readonly struct Rect
{
    public double XMin { get; }
    public double XMax => XMin + Size.Width;
    public double YMin { get; }
    public double YMax => YMin + Size.Height;
    public Size2D Size { get; }
    public Rect(double x, double y, Size2D size)
    {
        XMin = x;
        YMin = y;
        Size = size;
    }
}