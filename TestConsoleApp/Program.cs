namespace Composite;

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
    public Color Color { get; set; }
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

// Same interface for a single specification or for multiple!
public abstract class ISpecification<T>
{
    public abstract bool IsSatisfied(T p);
}

public class ColorSpecification : ISpecification<Product>
{
    private Color color;

    public ColorSpecification(Color color)
    {
        this.color = color;
    }

    public override bool IsSatisfied(Product p)
    {
        return p.Color == color;
    }
}

public class SizeSpecification : ISpecification<Product>
{
    private Size size;

    public SizeSpecification(Size size)
    {
        this.size = size;
    }

    public override bool IsSatisfied(Product product)
    {
        return product.Size == size;
    }
}

public abstract class CompositeSpecification<T> : ISpecification<T>
{
    // assignment to the field can only occur as part of the declaration or in a constructor in the same class
    protected readonly ISpecification<T>[] items;

    public CompositeSpecification(params ISpecification<T>[] items)
    {
        this.items = items;
    }
}

public class AndSpecification<T> : CompositeSpecification<T> // Combinator
{
    public AndSpecification(params ISpecification<T>[] items) : base(items)
    {
    }

    public override bool IsSatisfied(T t)
    {
        return items.All(i => i.IsSatisfied(t));
    }
}

public class OrSpecification<T> : CompositeSpecification<T> // Combinator
{
    public OrSpecification(params ISpecification<T>[] items) : base(items)
    {
    }

    public override bool IsSatisfied(T t)
    {
        return items.Any(i => i.IsSatisfied(t));
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

public class Demo
{
    static void Main(string[] args)
    {
        var apple = new Product("Apple", Color.Green, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var house = new Product("House", Color.Blue, Size.Large);

        Product[] products = { apple, tree, house };    
        
        var betterFilter = new BetterFilter();

        Console.WriteLine("Green products:");
        var greenProducts = betterFilter.Filter(products, new ColorSpecification(Color.Green));
        foreach (var product in greenProducts)
        {
            Console.WriteLine($" - {product.Name} is green");
        }

        Console.WriteLine("Large Blue products:");
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