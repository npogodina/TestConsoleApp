using System.Text;

namespace DesignPatterns;

// Builder pattern
public class Program
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        public const int Indentation = 2;

        public HtmlElement()
        {

        }

        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var indentation = new string(' ', Indentation * indent);
            sb.AppendLine($"{indentation}<{Name}>");

            if (!String.IsNullOrWhiteSpace(Text))
            {
                var textIndentation = new string(' ', Indentation * (indent + 1));
                sb.AppendLine($"{textIndentation}{Text}");
            }

            foreach (var element in Elements)
            {
                var elementString = element.ToStringImpl(indent + 1);
                sb.AppendLine(elementString);
            }

            sb.Append($"{indentation}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(1);
        }
    }

    public class HtmlBuilder
    {
        private readonly string rootName;
        public HtmlElement Root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            this.Root.Name = rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var child = new HtmlElement(childName, childText);
            Root.Elements.Add(child);
            return this;
        }

        public override string ToString()
        {
            return Root.ToString();
        }

        public void Clear()
        {
            Root = new HtmlElement() { Name = rootName };
        }
    }

    static void Main(string[] args)
    {
        var hello = "hello";
        var sb = new StringBuilder();
        sb.Append("<p>");
        sb.Append(hello);
        sb.Append("</p>");
        Console.WriteLine(sb);

        var words = new[] {"hello", "world"};
        sb.Clear();

        sb.Append("<ul>");
        foreach (var word in words)
        {
            sb.AppendFormat("<li>{0}</li>", word);
        }
        sb.Append("</ul>");
        Console.WriteLine(sb);

        var builder = new HtmlBuilder(rootName: "ul");
        builder.AddChild("li", "hello").AddChild("li", "world");
        Console.WriteLine(builder.ToString());
    }
}