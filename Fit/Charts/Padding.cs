namespace Fit.Charts;

public readonly struct Padding (double top = 0, double left = 0, double right = 0, double bottom = 0)
{
    public double Top { get; } = top;
    public double Left { get; } = left;
    public double Right { get; } = right;
    public double Bottom { get; } = bottom;
}
