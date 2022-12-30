using System.Text;

namespace ObjectTracking;

public interface ITheme
{
    string TextColor { get; }
    string BgrColor { get; }
}

public class LightTheme : ITheme
{
    public string TextColor => "Black";
    public string BgrColor => "White";
}

public class DarkTheme : ITheme
{
    public string TextColor => "White";
    public string BgrColor => "Dark Gray";
}

public class TrackingThemeFactory
{
    // About weak references:
    // https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/weak-references
    private readonly List<WeakReference<ITheme>> themes = new();
    public ITheme CreateTheme(bool dark)
    {
        ITheme theme = dark ? new DarkTheme() : new LightTheme();
        themes.Add(new WeakReference<ITheme>(theme));
        return theme;
    }

    public string Info
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var reference in themes)
            {
                if (reference.TryGetTarget(out var theme))
                {
                    bool dark = theme is DarkTheme;
                    sb.Append(dark ? "Dark" : "Light");
                    sb.AppendLine(" Theme");
                }
            }
            return sb.ToString();
        }
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        var factory = new TrackingThemeFactory();
        var theme1 = factory.CreateTheme(false);
        var theme2 = factory.CreateTheme(true);
        Console.WriteLine(factory.Info);
    }
}