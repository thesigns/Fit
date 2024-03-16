namespace Fit.Charts;

public readonly struct Size2D (double width, double height)
{
    public double Width { get; } = width;
    public double Height { get; } = height;
}
