public class Activity
{
    private List<string> _menuOptions = new List<string>();
    private string _userActivityChoiceNumber;
    private int _countdown;

    public Activity()
    {
        // Set menu options
        // Menu Options:
        // 1. Start breathing activity
        // 2. Start reflecting activity
        // 3. Start listing activity
        // 4. Quit
        // Select a choice from the menu: (user input)
        _menuOptions.AddRange(new List<string> { "1. Start breathing activity", "2. Start reflecting activity", "3. Start listing activity", "4. Quit" });
    }

    private void DisplayMenu()
    {
        Console.WriteLine("Menu Options:");
        foreach (string menuOption in _menuOptions)
        {
            Console.WriteLine(menuOption);
        }
        Console.Write("Select a numerical choice from the menu: ");
        _userActivityChoiceNumber = Console.ReadLine();
        // write error handling to remove all extra characters except the number
    }

    protected void WelcomeMessage(string activityNumber)
    {
        switch (activityNumber)
        {
            case "1":
                Console.WriteLine("Welcome to the Breathing Activity.");
                break;
            case "2":
                Console.WriteLine("Welcome to the Reflecting Activity.");
                break;
            case "3":
                Console.WriteLine("Welcome to the Listing Activity.");
                break;
        }
    }

    protected void Animation(int durationInSeconds)
    {
        Console.CursorVisible = false;

        char[] spinnerFrames = { '|', '/', '-', '\\' };
        int frameDelay = 100; // milliseconds between frames
        int totalFrames = (durationInSeconds * 1000) / frameDelay;

        for (int i = 0; i < totalFrames; i++)
        {
            Console.Write(spinnerFrames[i % spinnerFrames.Length]);
            Thread.Sleep(frameDelay);
            Console.Write("\b");
        }

        Console.Write(" "); // clear spinner
        Console.Write("\b"); // move back again
        Console.CursorVisible = true;
    }

    protected void GoodbyeMessage()
    {
        Console.WriteLine("\nWell done!!\n");
    }

    protected int SetCountdown()
    {
        Console.Write("\nHow long, in seconds, would you like for your session? (Please use increments of 10): ");
        _countdown = int.Parse(Console.ReadLine());
        return _countdown;
    }

    protected int GetCountdown()
    {
        return _countdown;
    }

    public void RunActivity()
    {
        BreathingActivity breathe = new BreathingActivity();
        ReflectingActivity reflect = new ReflectingActivity();
        ListingActivity list = new ListingActivity();
        while (_userActivityChoiceNumber != "4")
        {
            DisplayMenu();
            if (_userActivityChoiceNumber == "1")
            {
                breathe.RunBreathingActivity();
                _userActivityChoiceNumber = ""; // reset user choice
            }

            else if (_userActivityChoiceNumber == "2")
            {
                reflect.RunReflectingActivity();
                _userActivityChoiceNumber = ""; // reset user choice
            }

            else if (_userActivityChoiceNumber == "3")
            {
                list.RunListingActivity();
                _userActivityChoiceNumber = ""; // reset user choice
            }
        }
        Environment.Exit(0);
    }
}