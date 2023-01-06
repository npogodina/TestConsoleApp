using System.Text;

namespace Singleton;

// Ambient Context pattern

// Assumes there is one thread
// Alternatively can write a per thread singleton implementation
// (each thread will have its own BuildingContext)

public sealed class BuildingContext : IDisposable
{
    public int WallHeight = 0;

    private static Stack<BuildingContext> stack = new Stack<BuildingContext>();

    static BuildingContext()
    {
        // Ensure there's at least one state
        new BuildingContext(3000);
    }

    public BuildingContext(int height)
    {
        WallHeight = height;
        stack.Push(this);
    }

    public static BuildingContext Current => stack.Peek();

    public void Dispose()
    {
        // not strictly necessary
        if (stack.Count > 1)
        {
            stack.Pop();
        }
    }
}

public class Building
{
    public readonly List<Wall> Walls = new List<Wall>();

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var wall in Walls)
        {
            sb.AppendLine(wall.ToString());
        }
        return sb.ToString();
    }
}

public struct Point
{
    private int X, Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
    }
}

public class Wall
{
    public Point Start, End;
    public int Height;

    public Wall(Point start, Point end)
    {
        Start = start;
        End = end;
        Height = BuildingContext.Current.WallHeight;
    }

    public override string ToString()
    {
        return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, " + $"{nameof(Height)}: {Height}";
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        var house = new Building();

        // ground floor
        house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
        house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 5000)));

        // first floor
        using (new BuildingContext(4000))
        {
            house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
            house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 5000)));
        }

        // back to the ground floor
        house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
        house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 5000)));

        Console.WriteLine(house.ToString());
    }
}