namespace DesignPatterns;

// SOLID: Dependency Inversion
// High-level parts of the system should not depend on low-level parts of the system directly
// They should depend on abstraction

public enum Relationship
{
    Parent,
    Child,
    Sibling
}

public class Person
{
    public string? Name;
}

public interface IRelationshipBrowser
{
    IEnumerable<Person> FindAllChildrenOf(string name);
}

// Low-level class
public class Relationships : IRelationshipBrowser
{
    private List<(Person, Relationship, Person)> relations
        = new List<(Person, Relationship, Person)>();

    public void AddParentAndChild(Person parent, Person child)
    {
        relations.Add((parent, Relationship.Parent, child));
        relations.Add((child, Relationship.Child, parent));
    }

    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
        foreach (var relation in relations)
        {
            if(relation.Item1.Name == name && relation.Item2 == Relationship.Parent)
            {
                yield return relation.Item3;
            }
        }
    }
}

// High-level portion; depends on the interface
// Low-level class Relationships can change its internals (for example, how it stores data)
public class Research
{
    public Research(IRelationshipBrowser browser, string parentName)
    {
        var children = browser.FindAllChildrenOf(parentName);
        foreach (var child in children)
        {
            Console.WriteLine($"{parentName} has a child named {child.Name}");
        }
    }

    static void Main(string[] args)
    {
        var parent = new Person { Name = "Cody" };
        var child1 = new Person { Name = "Evelynn" };
        var child2 = new Person { Name = "Teddy" };

        var relationships = new Relationships();
        relationships.AddParentAndChild(parent, child1);
        relationships.AddParentAndChild(parent, child2);

        new Research(relationships, parent.Name);
    }
}