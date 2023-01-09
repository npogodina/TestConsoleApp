using MoreLinq;
using System.Collections.ObjectModel;

namespace Adapter;

// Adapts an interface you got to the interface you have

public class Point
{
    public int X, Y;

	public Point(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

    public override string ToString()
    {
        return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
    }
}

public class Line
{
    public Point Start, End;

	public Line(Point start, Point end)
	{
		this.Start = start;
		this.End = end;
	}
}

public class VectorObject : Collection<Line>
{

}

public class VectorRectangle : VectorObject
{
	public VectorRectangle(int x, int y, int width, int height)
	{
        Add(new Line(new Point(x, y), new Point(x + width, y)));
        Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
        Add(new Line(new Point(x, y), new Point(x, y + height)));
        Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
    }
}


// ? Assumes lines can only be vertical or horizontals 
// (for a rectangle, no diagonals)
public class LineToPointAdapter : Collection<Point>
{
	/// <summary>
	/// Counts invocations
	/// </summary>
	private static int count = 0;

	public LineToPointAdapter(Line line)
	{
        Console.WriteLine($"\n{++count}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}] (no caching)");

		int left = Math.Min(line.Start.X, line.End.X);
		int right = Math.Max(line.Start.X, line.End.X);
		int bottom = Math.Min(line.Start.Y, line.End.Y);
		int top = Math.Max(line.Start.Y, line.End.Y);

        // dx and dy denote change in x and y coordinates between the start and end points of a line.
		// dx = 0 implies the line is vertical, dy = 0 implies it is horizontal.

        int dx = right - left;
		int dy = top - bottom;

		if (dx == 0)
		{
			for (int y = bottom; y <= top; y++)
			{
				Add(new Point(left, y));

			}
		} else if (dy == 0)
		{
			for (int x = left; x <= right; x++)
			{
				Add(new Point(x, top));
			}
		}
    }
}

public class Demo
{
	private static readonly List<VectorObject> vectorObjects = new List<VectorObject>
	{
		new VectorRectangle(1, 1, 10, 10),
		new VectorRectangle(3, 3, 6, 6)
	};

	// the interface we have
	public static void DrawPoint(Point p)
	{
		Console.Write(".");
	}

	public static void Main(string[] args)
	{
		//DrawPoint(new Point(1, 1));
		Draw();
	}

    private static void Draw()
    {
		foreach (var vectorObj in vectorObjects)
		{
			foreach (var line in vectorObj)
			{
				var adapter = new LineToPointAdapter(line);
				adapter.ForEach(DrawPoint); // MoreLinq

			}
		}
    }
}