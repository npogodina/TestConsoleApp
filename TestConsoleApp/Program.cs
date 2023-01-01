namespace Prototype;

// Prototype = a partially or fully initialized object that you (deep) copy (clone) and make use of it.
// In a convenient manner (for example, via Factory)

// Deep cloning = cloning all inner fields recursively
// Shallow cloning = copying references

// Explicit Deep Copy Interface

public interface IPrototype<T>
{
    T DeepCopy();
}

public class Person : IPrototype<Person>
{
    public string[] Names;
    public Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public Person DeepCopy()
    {
        var namesCopy = new string[Names.Length];
        Array.Copy(Names, namesCopy, Names.Length);
        return new Person(namesCopy, Address.DeepCopy());
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
    }
}

public class Address : IPrototype<Address>
{
    public string StreetName;
    public int HouseNumber;

    public Address(Address other)
    {
        StreetName = other.StreetName;
        HouseNumber = other.HouseNumber;
    }

    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
    }

    public Address DeepCopy()
    {
        return new Address(StreetName, HouseNumber);
    }

    public override string ToString()
    {
        return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        var john = new Person(
            new string[] { "John", "Smith" },
            new Address("London street", 123));

        Console.WriteLine($"John = {john.ToString()}");

        var jane = john.DeepCopy();
        Console.WriteLine($"Jane Copied from John = {jane.ToString()}");

        jane.Names[0] = "Jane";
        jane.Address.HouseNumber = 456;
        Console.WriteLine($"Jane Modified = {jane.ToString()}");
        Console.WriteLine($"John = {john.ToString()}");
    }
}