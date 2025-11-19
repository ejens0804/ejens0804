using System.ComponentModel;

public class ScriptureUserMenu
{
    private string _userChoice;
    private string _keepRunningChoice = "";

    public ScriptureUserMenu()
    {
        // write a menu that allows the user to enter in a new scripture
        // including the reference and words
        // also have the user pick between exiting the program and hiding more words
        Console.WriteLine("Welcome to the Scripture Memorizer!\n");

        Console.WriteLine("Menu Options:");
        Console.WriteLine("1. Use preloaded scripture");
        Console.WriteLine("2. Input your own scripture");
        Console.WriteLine("3. Exit Program");
        Console.Write("Please type the menu number of the option you have selected: ");

        _userChoice = Console.ReadLine();
        }

    public int RunUserChoice()
    {
        // write a for each loop using a number from the user
        // to determine how many times to run the add scripture to scripture
        // words dictionary
        switch (_userChoice)
        {
            case "1": // use preloaded scripture
                Reference ref1 = new Reference();
                ref1.DisplayReference();
                ScriptureWords scripWords = new ScriptureWords();
                scripWords.DisplayAllWordsInScripture();
                return 1;

            case "2": // user input scripture
                RunUserInputScripture();
                return 2;

            case "3": // exit program
                Environment.Exit(0);
                break;
                

        }
        return 0;
        
    }

    public void PreloadedKeepRunningUntilNoMoreWords()
    {
        
        ScriptureWords scripWords1 = new ScriptureWords();
        WordHider wh = new WordHider();
        Reference ref1 = new Reference();
        List<string> individualWordList = scripWords1.CombineScriptureDictionaryWords();
        while (_keepRunningChoice != "quit")
        {
            
            Console.WriteLine($"\bPress enter to hide words or type 'quit' to exit the program:");
            _keepRunningChoice = Console.ReadLine();
            _keepRunningChoice = _keepRunningChoice.ToLower();
            if (_keepRunningChoice == "")
            {
                // Console.Clear();
                ref1.DisplayReference();
                List<string> wordsToHide = wh.PickWordsToHide(individualWordList);
                scripWords1.DisplayRemainingWordsInScripture(wordsToHide);
                // remove the hidden words from the available word list
                foreach (string word in wordsToHide)
                {
                    individualWordList.Remove(word);
                }
                if (scripWords1.CheckIfAllWordsAreHidden() == true)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
    
    private void RunUserInputScripture()
    {
        Console.Write("Please type the Book for your scripture reference: ");
        string book = Console.ReadLine();

        Console.Write("\bPlease type the Chapter/Section: ");
        string chapter = Console.ReadLine();

        Console.Write("\bPlease type the Starting verse number: ");
        int verseStart = int.Parse(Console.ReadLine());

        Console.Write("\bPlease type the Ending verse number (type 0 if you only have 1 verse): ");
        int verseEnd = int.Parse(Console.ReadLine());

        Reference ref1 = new Reference(book, chapter, verseStart, verseEnd);
        int verseQuantity = (verseEnd - verseStart) + 1;
        if (verseEnd == 0)
        {
            verseQuantity = 1;
        }
        
        ScriptureWords sw = new ScriptureWords(verseQuantity);
        
        ref1.DisplayReference();

        sw.DisplayAllWordsInUserScripture(book, chapter, verseStart, verseEnd);

        WordHider wh = new WordHider();
        List<string> individualWordList = sw.CombineScriptureDictionaryWords();
        while (_keepRunningChoice != "quit")
        {
            
            Console.WriteLine($"\bPress enter to hide words or type 'quit' to exit the program:");
            _keepRunningChoice = Console.ReadLine();
            _keepRunningChoice = _keepRunningChoice.ToLower();
            if (_keepRunningChoice == "")
            {
                // Console.Clear();
                List<string> wordsToHide = wh.PickWordsToHide(individualWordList);
                ref1.DisplayReference();
                sw.DisplayRemainingWordsInUserScripture(wordsToHide, book, chapter, verseStart, verseEnd);
                // remove the hidden words from the available word list
                foreach (string word in wordsToHide)
                {
                    individualWordList.Remove(word);
                }
                if (sw.CheckIfAllWordsAreHidden() == true)
                {
                    Console.WriteLine("\b\bAll words are hidden\b\b");
                    Environment.Exit(0);

                }
            }
        }

    }
}