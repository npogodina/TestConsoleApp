namespace DesignPatterns;

// SOLID: Interface Segregation
// Interfaces shouldn't be too large
// Don't make consumers implement methods they don't need
// (don't make them pay for what they don't need)
// Have multiple small interfaces instead

public class Program
{
    public interface IPrinter
    {
        void Print();
    }

    public interface IScanner
    {
        void Scan();
    }

    public interface IMultiFunctionDevice : IPrinter, IScanner
    {

    }

    public class MultiFunctionDevice : IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionDevice(IPrinter printer, IScanner scanner)
        {
            this.printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
            this.scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(scanner));
        }

        public void Print()
        {
            printer.Print(); // Decorator pattern
        }

        public void Scan()
        {
            scanner.Scan(); // Decorator pattern
        }
    }

    static void Main(string[] args)
    {


    }
}