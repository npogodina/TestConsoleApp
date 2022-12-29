namespace Coding.Exercise;

// Async Factory method allows to initialize object fully and in asynchronous manner

public class Foo
{
    private Foo()
    {

    }

    private async Task<Foo> InitAsync()
    {
        await Task.Delay(1000);
        return this;
    }

    public static Task<Foo> CreateAsync()
    {
        var foo = new Foo();
        return foo.InitAsync();
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        Foo x = await Foo.CreateAsync();
    }
}
