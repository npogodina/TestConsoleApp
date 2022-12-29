namespace Coding.Exercise;

public class Foo
{
    public Foo()
    {

    }

    // One way to have an async operation in Constructor
    public async Task<Foo> InitAsync()
    {
        await Task.Delay(1000);
        return this;
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        var foo = new Foo();
        await foo.InitAsync();
    }
}
