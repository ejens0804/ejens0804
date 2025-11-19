public class JournalEntry
{
    public string _journalEntry;
    public string _promptUsed;

    public string _formattedEntry;

    public DateTime _date;
    public void WriteJournalEntry()
    {
        PromptGenerator promptForCurrentEntry = new PromptGenerator();
        _promptUsed = promptForCurrentEntry.GeneratePrompt();
        _journalEntry = Console.ReadLine();
    }

    public void DisplayJournalEntry()
    {
        _date = DateTime.Now;
        Console.WriteLine($"\n{_date}\n{_promptUsed}\n{_journalEntry}");
    }

    public string FormatEntryForFileSave()
    {
        _formattedEntry = $"{_date} ~|~ {_promptUsed} ~|~ {_journalEntry}";
        return _formattedEntry;
        // return _date.ToString(), _promptUsed, _journalEntry;
    }
}