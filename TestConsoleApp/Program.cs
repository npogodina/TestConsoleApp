using System.Text;

namespace Composite;

// Allows to treat both single objects and composite objects (grouping of objects) uniformly
// For example, Foo and Collection<Foo> have common APIs

public class GraphicObject
{
    public virtual string Name { get; set; } = "Group";
    public string Color;


    private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();
    public List<GraphicObject> Children => children.Value;

    public override string ToString()
    {
        var sb = new StringBuilder();
        Print(sb, 0);
        return sb.ToString();
    }

    private void Print(StringBuilder sb, int depth)
    {
        sb.Append(new string('>', depth))
          .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ") // continue here
          .AppendLine(Name);

        foreach (var child in Children)
        {
            child.Print(sb, depth + 1);
        }
    }
}

public class Circle : GraphicObject
{
    public override string Name => "Circle";
}

public class Square : GraphicObject
{
    public override string Name => "Square";
}

public class Demo
{
    static void Main(string[] args)
    {
        var drawing = new GraphicObject { Name = "My Drawing" };
        drawing.Children.Add(new Square { Color = "Red" });
        drawing.Children.Add(new Circle { Color = "Yellow" });

        var group = new GraphicObject();
        group.Children.Add(new Circle { Color = "Blue" });
        group.Children.Add(new Square { Color = "Blue" });
        drawing.Children.Add(group);

        Console.WriteLine(drawing);
    }
}