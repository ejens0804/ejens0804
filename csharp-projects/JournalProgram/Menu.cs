public class Menu
{
    public List<string> _menuItem = new List<string> {"1. Write New Entry","2. Display Journal Entries","3. Save to File","4. Load entries from File","5. List Saved Journals","6. Exit program"};
    public string _menuChoice;
    Journal journal1 = new Journal();
    FileFinder ff = new FileFinder();

    public void DisplayMenu()
    {
        Console.WriteLine($"\nJournal Program Menu\nPlease choose one of the following options:\n");
        foreach (string menuOptions in _menuItem)
        {
            Console.WriteLine(menuOptions);
        }   
    }

    public string GetUserMenuChoice()
    {
        Console.Write("\nPlease select one of the numbered options above:");
        _menuChoice = Console.ReadLine();
        return _menuChoice;
    }

    public void RunMenuChoice(string menuNumber)
    {
        //1. Write New Entry
        //2. Display Journal Entries
        // 3. Save to File
        // 4. Load entries from File
        // 5. Exit Program

        JournalEntry journalEntryForToday = new JournalEntry();

        if (menuNumber == "1") //Write new entry
        {
            journalEntryForToday.WriteJournalEntry();
            journal1._entries.Add(journalEntryForToday);
        }

        else if (menuNumber == "2") //Display Journal Entries
        {
            journal1.DisplayJournalEntries();
        }

        else if (menuNumber == "3") //Save to File
        {
            journal1.SaveToFile();
        }

        else if (menuNumber == "4") //Load entries from file
        {
            journal1.LoadFromFile();
        }

        else if (menuNumber == "5") //List previously saved journals
        {
            ff.DisplayFileListOfSavedJournals();
        }

        else if (menuNumber == "6") //Exit program
        {
            Console.WriteLine("Thank you for using the Journal Program");
            Environment.Exit(0);
        }
        // else if (string.IsNullOrWhiteSpace(menuNumberString))
        // {
        //     Console.WriteLine("Input cannot be empty. Please try again");
        //     return 0;
        // }
    }
}