namespace AbstractFactory;

public enum HotDrink
{
    Tea,
    Coffee
}

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
    public enum AvailableDrink
    {
        Tea,
        Coffee
    }

    private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
        new Dictionary<AvailableDrink, IHotDrinkFactory>();

    public HotDrinkMachine()
    {
        foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        {
            var factory = (IHotDrinkFactory)Activator.CreateInstance(
                Type.GetType("AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));

            factories.Add(drink, factory);
        }
    }

    public IHotDrink MakeDrink(AvailableDrink type, int amount)
    {
        var machine = factories[type];
        var drink = machine.Prepare(amount);
        return drink;
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        var machine = new HotDrinkMachine();
        var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 200);
        drink.Consume();
    }
}