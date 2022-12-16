namespace DesignPatterns;

// SOLID: Open Closed Principle
// Open to extension and closed to modification.
// We should not modify existing classes once shipped.
// We could ship new modules adding new functionality via inheritance through interfaces.

public enum Color
{
    Red, Blue, Green
}

public enum Size
{
    Small, Medium, Large
}

public class Product
{
    public string Name;
    public Color Color;
    public Size Size;

    public Product(string name, Color color, Size size)
    {
        if (name == null)
        {
            throw new ArgumentNullException(paramName: nameof(name));
        }

        Name = name;
        Color = color;
        Size = size;
    }
}

public class ProductFilter
{
    public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    {
        foreach (var p in products) 
        {
        if (p.Size == size)
            {
                yield return p;
            }
        }
    }

    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
        foreach (var p in products)
        {
            if (p.Color == color)
            {
                yield return p;
            }
        }
    }
}

public interface ISpecification<T>
{
    bool IsSatisfied(T t);
}

public class ColorSpecification : ISpecification<Product>
{
    private Color color;

    public ColorSpecification(Color color)
    {
        this.color = color;
    }

    public bool IsSatisfied(Product product)
    {
        return product.Color == color;
    }
}

public class SizeSpecification : ISpecification<Product>
{
    private Size size;

    public SizeSpecification(Size size)
    {
        this.size = size;
    }

    public bool IsSatisfied(Product product)
    {
        return product.Size == size;
    }
}

public class AndSpecification<T> : ISpecification<T> // Combinator
{
    private ISpecification<T> first, second;

    public AndSpecification(ISpecification<T> first, ISpecification<T> second)
    {
        this.first = first ?? throw new ArgumentNullException(nameof(first));
        this.second = second ?? throw new ArgumentNullException(nameof(second));
    }

    public bool IsSatisfied(T t)
    {
        return first.IsSatisfied(t) && second.IsSatisfied(t);
    }
}

public interface IFilter<T>
{
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
}

public class BetterFilter : IFilter<Product>
{
    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
    {
        foreach (var item in items)
        {
            if (specification.IsSatisfied(item))
            {
                yield return item;
            }
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var apple = new Product("Apple", Color.Green, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var house = new Product("House", Color.Blue, Size.Large);

        Product[] products = { apple, tree, house };
        
        Console.WriteLine("Green products (old):");
        var pf = new ProductFilter();

        foreach (var p in pf.FilterByColor(products, Color.Green))
        {
           Console.WriteLine($" - {p.Name} is green");
        }

        var betterFilter = new BetterFilter();

        Console.WriteLine("Green products (new):");

        var greenProducts = betterFilter.Filter(products, new ColorSpecification(Color.Green));

        foreach (var product in greenProducts)
        {
            Console.WriteLine($" - {product.Name} is green");
        }


        Console.WriteLine("Large Blue products (new):");

        var largeBlueProducts = betterFilter.Filter(
            products, 
            new AndSpecification<Product>(
                new ColorSpecification(Color.Blue), 
                new SizeSpecification(Size.Large)
            ));

        foreach (var product in largeBlueProducts)
        {
            Console.WriteLine($" - {product.Name} is large and blue");
        }
    }
}