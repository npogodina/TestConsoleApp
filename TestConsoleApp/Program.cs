using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Prototype;

// Copy through serialization
// Different mechanisms can be used: XML, JSON, etc.

public static class ExtensionMethods
{
    // Do not use BinaryFormatter!
    // Due to deserialization vulnarabilities
    // https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide

    //public static T DeepCopy<T>(this T self)
    //{
    //    var stream = new MemoryStream();
    //    var formatter = new BinaryFormatter();
    //    formatter.Serialize(stream, self);
    //    stream.Seek(0, SeekOrigin.Begin);
    //    object copy = formatter.Deserialize(stream);
    //    stream.Close();
    //    return (T)copy;
    //}

    public static T DeepCopyXml<T>(this T self)
    {
        using (var ms = new MemoryStream()) //  to ensure that the object is disposed as soon as it goes out of scope
        {
            var s = new XmlSerializer(typeof(T));
            s.Serialize(ms, self);
            ms.Position = 0;
            return (T) s.Deserialize(ms);
        }
    }

    public static T DeepCopyJson<T>(this T self)
    {
        string json = JsonConvert.SerializeObject(self);
        return JsonConvert.DeserializeObject<T>(json);
    }
}

public class Person
{
    public string[] Names;
    public Address Address;

    public Person()
    {

    }

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
    }
}

public class Address
{
    public string StreetName;
    public int HouseNumber;

    public Address()
    {

    }

    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
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

        var jane = john.DeepCopyXml();
        
        // Alternative:
        //var jane = john.DeepCopyJson();

        Console.WriteLine($"Jane Copied from John = {jane.ToString()}");

        jane.Names[0] = "Jane";
        jane.Address.HouseNumber = 456;
        Console.WriteLine($"Jane Modified = {jane.ToString()}");
        Console.WriteLine($"John = {john.ToString()}");
    }
}