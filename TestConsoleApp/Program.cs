namespace DesignPatterns;

// SOLID: Liscov Substitution Principle
// We should be able to substitute a base type for a sub type.
// You should be able to upcast to your base type.

public class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }

    public Rectangle()
    {

    }

    public Rectangle(int width, int height)
    {
        this.Width = width; 
        this.Height = height;
    }

    public override string ToString()
    {
        return $"{nameof(Width)}: {Width}; {nameof(Height)}: {Height}";
    }
}

public class Square : Rectangle
{
    public override int Width
    {
        set
        {
            base.Width = base.Height = value;
        }
    }

    public override int Height
    {
        set
        {
            base.Height = base.Width = value;
        }
    }
}

public class Program
{
    public static int Area(Rectangle rectangle) => rectangle.Width * rectangle.Height;

    static void Main(string[] args)
    {

        var rc = new Rectangle(2, 3);
        Console.WriteLine($"{rc}; {nameof(Area)}: {Area(rc)}");

        Rectangle square = new Square();
        square.Width = 3;
        Console.WriteLine($"{square}; {nameof(Area)}: {Area(square)}");

    }

    
}