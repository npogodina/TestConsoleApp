namespace DesignPatterns;

// Faceted Builder pattern

public class Person
{
    // Address
    public string StreetAddress, Postcode, City;

    // Employment
    public string Company, Position;
    public int Salary;
}

public class PersonBuilder // Facade
{
    // reference!
    protected Person person = new Person();

    public PersonJobBuilder Works => new PersonJobBuilder(person);

    public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

    //public static implicit operator Person(PersonBuilder pb)
    //{
    //    return pb.person;
    //}

    public Person BuildPerson()
    {
        return person;
    }
}

// can be inner classes of PersonBuilder to hind internals from the consumer

public class PersonJobBuilder : PersonBuilder
{
    public PersonJobBuilder(Person person)
    {
        this.person = person;
    }

    public PersonJobBuilder At(string company)
    {
        this.person.Company = company;
        return this;
    }

    public PersonJobBuilder As(string position)
    {
        this.person.Position = position;
        return this;
    }

    public PersonJobBuilder Making(int salary)
    {
        this.person.Salary = salary;
        return this;
    }
}

public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person)
    {
        this.person = person;
    }

    public PersonAddressBuilder Street(string street)
    {
        this.person.StreetAddress = street;
        return this;
    }

    public PersonAddressBuilder Postcode(string postcode)
    {
        this.person.Postcode = postcode;
        return this;
    }

    public PersonAddressBuilder City(string city)
    {
        this.person.City = city;
        return this;
    }
}
   
public class Program
{
    static void Main(string[] args)
    {
        var pb = new PersonBuilder();
        Person person = pb
            .Works
                .At("Microsoft")
                .As("Software Engineer")
                .Making(140000)
            .Lives
                .Street("33333 333 DR SE")
                .Postcode("33333")
                .City("Monroe")
            .BuildPerson();              
    }
}