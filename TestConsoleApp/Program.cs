using Autofac;

namespace Bridge;

// Mechanism that decouples an interface hierarchy from an implementation hierarchy

// Connects components together through abstractions (Interface or Abstract classes)
// Allows to avoid entity explosion 

public interface IRenderer
{
    void RenderCircle(float radius);
}

public class VectorRenderer : IRenderer
{
    public void RenderCircle(float radius)
    {
        Console.WriteLine($"Drawing a circle of radius {radius}");
    }
}

public class RasterRenderer : IRenderer
{
    public void RenderCircle(float radius)
    {
        Console.WriteLine($"Drawing pixels for circle of radius {radius}");
    }
}

public abstract class Shape
{
    protected IRenderer renderer;

    // Bridge between the shape that's being drawn and the component that draws it
    public Shape(IRenderer renderer)
    {
        this.renderer = renderer;
    }

    public abstract void Draw();
    public abstract void Resize(float factor);
}

public class Circle : Shape
{
    private float radius;

    public Circle(IRenderer renderer, float radius) : base(renderer)
    {
        this.radius = radius;
    }

    public override void Draw()
    {
        renderer.RenderCircle(radius);
    }

    public override void Resize(float factor)
    {
        radius = radius * factor;
    }
}

public class Demo
{
    static void Main(string[] args)
    {
        //var raster = new RasterRenderer();
        //var circle = new Circle(raster, 5);
        //circle.Draw();
        //circle.Resize(2);
        //circle.Draw();

        var cb = new ContainerBuilder();
        cb.RegisterType<VectorRenderer>().As<IRenderer>().SingleInstance();
        cb.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));

        using (var c = cb.Build())
        {
            var circle = c.Resolve<Circle>(new PositionalParameter(0, 5.0f));
            circle.Draw();
            circle.Resize(2);
            circle.Draw();
        }
    }
}