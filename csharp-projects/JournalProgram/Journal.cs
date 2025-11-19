using System.Formats.Asn1;

public class Journal
{
    public List<JournalEntry> _entries = new List<JournalEntry>();

    public string _name = "Ethan Jensen";

    public void AddJournalEntryToList()
    { 

    }

    public void DisplayJournalEntries()
    {
        foreach (JournalEntry entry in _entries)
        {
            entry.DisplayJournalEntry();
        }

    }

    public void SaveToFile()
    {
        Console.Write("What is the file name you would like to save? (use .csv for file suffix): ");
        string fileName = Console.ReadLine();
        using (StreamWriter stmWriter = new StreamWriter(fileName))
        {
            stmWriter.WriteLine("Date ~|~ Prompt ~|~ Entry");
            foreach (JournalEntry entry in _entries)
            {
                stmWriter.WriteLine(entry.FormatEntryForFileSave());
            }
            stmWriter.Close();
        }
        Console.WriteLine($"Your journal has been saved to the file: {fileName}");
    }

    public void LoadFromFile()
    {
        Console.Write("What .csv journal file would you like to read?");
        string fileName = Console.ReadLine();

        // string[] lines = System.IO.File.ReadAllLines(fileName);

        using (StreamReader stmReader = new StreamReader(fileName))
        {
            // foreach (JournalEntry entry in _entries)
            foreach (string entry in File.ReadLines(fileName))
            {
                string[] csvFileLines = entry.Split(" ~|~ "); //use the ~|~ delimiter as the separation between values and remove it
                string date = csvFileLines[0];
                string prompt = csvFileLines[1];
                string entryResponse = csvFileLines[2];
                Console.WriteLine($"\n{date} \n{prompt} \n{entryResponse}");
            }

        }
    }

}