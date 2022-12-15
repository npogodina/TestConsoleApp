using System.Diagnostics;

namespace DesignPatterns;

// SOLID: Single Responsibility Principle

/// <summary>
/// Dedicated class to create journal and journal entries
/// </summary>
public class Journal
{
    private readonly List<string> entries = new List<string>();

    private static int count = 0;

    public int AddEntry(string text)
    {
        count++;
        entries.Add($"{count}: {text}");
        return count; // memento
    }

    public void RemoveEntry(int index)
    {
        entries.RemoveAt(index);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, entries);
    }
}

/// <summary>
/// Dedicated class to save journal entries
/// </summary>
public class Persistance
{
    public void SaveToFile(Journal journal, string filename, bool overwrite = false) 
    {
        if (overwrite || !File.Exists(filename)) 
        {
            var content = journal.ToString();
            File.WriteAllText(filename, content);
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        journal.AddEntry("My first entry");
        journal.AddEntry("My second entry");
        journal.AddEntry("My third entry");
        //journal.RemoveEntry(1);
        Console.WriteLine(journal);

        Persistance persistance = new Persistance();
        var filename = @"C:\Temp\TestJournal.txt";
        persistance.SaveToFile(journal, filename, true);

        var p = new Process();
        p.StartInfo = new ProcessStartInfo(filename)
        {
            UseShellExecute = true
        };
        p.Start();
    }
}