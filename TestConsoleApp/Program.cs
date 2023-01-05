using MoreLinq;

namespace Singleton;

// Singleton = controversial design pattern
// Component that is instatiated only once.

// Motivation:
// 1) For some components it makes sense to have only one in the system
// - database repository
// - object factory
// 2) Constructor call is expensive. We want to:
// - do it once
// - provide everyone with the same instance
// 3) Want to prevent anyone from creating additinal copies
// 4) Need to take care of lazy instantiation and threat safety

public interface IDatabase
{
    int GetPopulation(string city);
}

public class SingletonDatabase : IDatabase
{
    private Dictionary<string, int> capitals;

    private SingletonDatabase()
    {
        Console.WriteLine("Initializing database...");
        capitals = File.ReadAllLines("Capitals.txt")
            .Batch(2)
            .ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1)));
    }

    // Use lazy initialization to defer the creation of a large or resource-intensive object,
    // or the execution of a resource-intensive task,
    // particularly when such creation or execution might not occur during the lifetime of the program.
    // https://learn.microsoft.com/en-us/dotnet/api/system.lazy-1?redirectedfrom=MSDN&view=net-7.0
    private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

    public static SingletonDatabase Instance => instance.Value;

    public int GetPopulation(string city)
    {
        return capitals[city];
    }
}

public class Demo
{
    static void Main()
    {
        var db = SingletonDatabase.Instance;

        // works just fine while you're working with a real database.
        var city = "Tokyo";
        Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
    }
}
