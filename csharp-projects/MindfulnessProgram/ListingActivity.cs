public class ListingActivity : Activity
{
    private string _activityDescription;
    private List<string> _promptList = new List<string>();
    private List<string> _userList = new List<string>();

    public ListingActivity()
    {
        // set variables here and don't hard code them directly
        _activityDescription = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
        _promptList.AddRange(new List<string> {"Who are people that you appreciate?", "What are personal strengths of yours?", "Who are people that you have helped this week?", "When have you felt the Holy Ghost this month?", "Who are some of your personal heroes?"});
    }

    private void DisplayActivityDescription()
    {
        Console.WriteLine($"\n{_activityDescription}");
    }

    private string GetPromptFromList()
    {
        Random rand = new Random();
        int index = rand.Next(_promptList.Count);
        string randomPrompt = _promptList[index];
        return randomPrompt;
    }

    private void AddUserItemToList(string userItem)
    {
        _userList.Add(userItem);
    }

    private int GetUserListCount()
    {
        return _userList.Count;
    }

    public void RunListingActivity()
    {
        _userList.Clear();
        Console.Clear();
        base.WelcomeMessage("2");
        DisplayActivityDescription();
        base.SetCountdown(); // Get countdown time length

        int countdown = base.GetCountdown(); // Return countdown to be used

        Console.WriteLine("Get ready... ");

        Console.WriteLine("\nList as many responses as you can to the following prompt: ");
        Console.WriteLine($"--- {GetPromptFromList()} ---");
        Console.Write("You may begin in: ");
        for (int i = 5; i > 0; i--) // Countdown for 5 seconds
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(countdown);
        DateTime currentTime = DateTime.Now;
        string userItem;

        while (currentTime < endTime)
        {
            currentTime = DateTime.Now;
            Console.Write("> ");
            userItem = Console.ReadLine();
            AddUserItemToList(userItem);
        }
        Console.WriteLine($"You listed {GetUserListCount()} items!");

        base.GoodbyeMessage();
        base.Animation(5);
        Console.WriteLine($"You have completed another {base.GetCountdown()} seconds of the Listing Activity.");
        base.Animation(5);
        Console.Clear();
    }
}