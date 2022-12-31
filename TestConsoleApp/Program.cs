namespace AbstractFactory;

// Honoring Open-Closed Principle by
// Replacing the inner enum of available drinks with Reflection

// Reflection
// Reflection provides objects (of type Type) that describe assemblies, modules, and types.
// You can use reflection to dynamically create an instance of a type,
// bind the type to an existing object,
// or get the type from an existing object and invoke its methods or access its fields and properties.
// (from: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection)

// Better approach: use Dependency Injection

public interface IHotDrink
{
    void Consume();
}

internal class Tea : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This tea is nice but I'd prefer it with milk.");
    }
}

internal class Coffee : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This coffee is delicious!");
    }
}

public interface IHotDrinkFactory
{
    IHotDrink Prepare(int amount);
}

internal class TeaFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Put in tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
        return new Tea();
    }
}

internal class CoffeeFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
        return new Coffee();
    }
}

public class HotDrinkMachine
{
    //public enum AvailableDrink
    //{
    //    Tea,
    //    Coffee
    //}

    //private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
    //    new Dictionary<AvailableDrink, IHotDrinkFactory>();

    //public HotDrinkMachine()
    //{
    //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
    //    {
    //        var factory = (IHotDrinkFactory)Activator.CreateInstance(
    //            Type.GetType("AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));

    //        factories.Add(drink, factory);
    //    }
    //}

    //public IHotDrink MakeDrink(AvailableDrink type, int amount)
    //{
    //    var machine = factories[type];
    //    var drink = machine.Prepare(amount);
    //    return drink;
    //}

    private List<Tuple<string, IHotDrinkFactory>> factories = 
        new List<Tuple<string, IHotDrinkFactory>>();

    public HotDrinkMachine()
    {
        foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
        {
            if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
            {
                factories.Add(Tuple.Create(
                    t.Name.Replace("Factory", string.Empty),
                    (IHotDrinkFactory)Activator.CreateInstance(t)));
            }
        }
    }

    public IHotDrink MakeDrink()
    {
        Console.WriteLine("Available drinks:");
        for (int i = 0; i < factories.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {factories[i].Item1}");
        }

        while (true)
        {
            Console.WriteLine("Please, enter drink number.");
            var userChoice = Console.ReadLine();
            if (userChoice != null &&
                int.TryParse(userChoice, out int drinkNumber) &&
                drinkNumber > 0 &&
                drinkNumber <= 2)
            {
                Console.WriteLine("Please, enter amount in ml.");
                var userAmount = Console.ReadLine();
                if (userAmount != null &&
                    int.TryParse(userAmount, out int amount) &&
                    amount > 0)
                {
                    Console.WriteLine("Thank you!");
                    var drink = factories[drinkNumber - 1].Item2.Prepare(amount);
                    return drink;
                }
            }

            Console.WriteLine("Incorrect input! Please, try again!");
        }
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        var machine = new HotDrinkMachine();
        
        //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 200);
        //drink.Consume();

        var drink = machine.MakeDrink();
        drink.Consume();
    }
}