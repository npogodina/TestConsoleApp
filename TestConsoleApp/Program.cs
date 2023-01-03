namespace Coding.Exercise
{
    public interface IDeepCopiable<T>
    {
        T DeepCopy();
    }


    public class Point : IDeepCopiable<Point>
    {
        public int X, Y;

        public Point DeepCopy()
        {
            return new Point
            {
                X = this.X,
                Y = this.Y
            };
        }
    }

    public class Line : IDeepCopiable<Line>
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            return new Line
            {
                Start = this.Start.DeepCopy(),
                End = this.End.DeepCopy()
            };
        }

        public override string ToString()
        {
            return $"Line: Start = {Start.X}, {Start.Y}; End = {End.X}, {End.Y}";
        }
    }

    public class Demo
    {
        public static void Main(string[] args)
        {
            var line = new Line
            {
                Start = new Point
                {
                    X = 1,
                    Y = 2
                },
                End = new Point
                {
                    X = 3,
                    Y = 4
                }
            };

            Console.WriteLine($"Original. {line.ToString()}");

            var lineCopy = line.DeepCopy();
            Console.WriteLine($"Copy. {lineCopy.ToString()}");

            lineCopy.Start.X = 9;
            Console.WriteLine($"Modified Copy. {lineCopy.ToString()}");

            Console.WriteLine($"Original. {line.ToString()}");
        }
    }
}