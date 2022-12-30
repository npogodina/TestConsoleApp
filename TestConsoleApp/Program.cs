namespace Coding.Exercise;

// Single Responcibility Principle:
// Instantiaing a class is a separate responcibility!
public static class PointFactory
{
    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }
}

public class Point
{
    private double x, y;

    // Can be called only to create a new cartesian point
    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var point = PointFactory.NewCartesianPoint(4.50, 6.00);
    }
}

