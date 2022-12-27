using System.Runtime.InteropServices;
using System.Text;

namespace DesignPatterns;

// Functional Builder pattern

public class Person
{
    public string Name, Position;
}

public sealed class PersonBuilder // sealed = can't inherit from the class
{
    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

    private PersonBuilder AddAction(Action<Person> action)
    {
        actions.Add(p => { action(p); return p; });
        return this;
    }

    public PersonBuilder Do(Action<Person> action) 
        => AddAction(action);

    public PersonBuilder Called(string name) 
        => Do(p => p.Name = name);

    public Person Build() 
        => actions.Aggregate(new Person(), (p, f) => f(p));
}

public static class PersonBuilderExtensions // extending without inheritance
{
    public static PersonBuilder WorksAs( this PersonBuilder builder, string position)
        => builder.Do(p => p.Position = position);
}

public class Program
{
    static void Main(string[] args)
    {
        var person = new PersonBuilder()
            .Called("Cody")
            .WorksAs("SRE")
            .Build();
    }
}