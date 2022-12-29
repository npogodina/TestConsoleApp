namespace Coding.Exercise
{
    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // Factory method
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        // Factory method:
        // Overload with the same number of arguments!
        // Unique names!
        // ( = API improvement)
        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var point = Point.NewCartesianPoint(4.50, 6.00);
        }
    }
}
