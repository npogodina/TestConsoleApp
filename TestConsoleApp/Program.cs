namespace PropertyVsSingletonField;

public class Point
{
    private double x, y;
    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    // Property; each time called will get a new Point
    public static Point Origin => new Point(0, 0);

    // Singleton field = better; Point initialized once
    public static Point Origin2 = new Point(0, 0);

    public static class Factory
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
}

public class Program
{
    static void Main(string[] args)
    {
        var point = Point.Factory.NewCartesianPoint(4.50, 6.00);
        var origin = Point.Origin;
        var origin2 = Point.Origin2;
    }
}