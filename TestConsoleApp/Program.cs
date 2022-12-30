namespace InnerFactory;

public class Point
{
    private double x, y;

    // Public can be called to create a new cartesian point (only)

    // Make internal if you are writing a library to prevent the consumers of the assembly to use it on the outside
    // (and keep Factory outside as a separate class)

    // Make private and move Factory inside Point to prevent using a constructor
    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

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
    }
}