namespace Decorator;

// Facilitates the addition of behaviors to individual objects without inheriing from them
// Useful when a class is sealed or when we want to inherit from multiple classes
//// (in C# a class cannot have multiple base classes)

public interface IBird
{
    int Weight { get; set; }

    void Fly();
}

public class Bird : IBird
{
    public void Fly()
    {
        Console.WriteLine($"Flying with weight {Weight}");
    }

    public int Weight { get; set; }
}

public interface ILizard
{
    int Weight { get; set; }

    void Crawl();
}

public class Lizard : ILizard
{
    public void Crawl()
    {
        Console.WriteLine($"Crawling with weight {Weight}");
    }

    public int Weight { get; set; }
}

public class Dragon : IBird, ILizard
{
    private Bird _bird = new Bird();
    private Lizard _lizard = new Lizard();
    private int weight;

    public void Crawl()
    {
        _lizard.Crawl();
    }

    public void Fly()
    {
        _bird.Fly();
    }

    // Workaround for situations when both classes implement the same property
    public int Weight
    {
        get
        {
            return weight;
        }
        set
        {
            weight = value;
            _bird.Weight = weight;
            _lizard.Weight = weight;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var dragon = new Dragon();
        dragon.Weight = 100;
        dragon.Fly();
        dragon.Crawl();
    }
}
