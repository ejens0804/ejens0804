public class ReflectingActivity : Activity
{

    private string _activityDescription;
    private List<string> _promptList = new List<string>();
    private List<string> _promptQuestions = new List<string>();

    public ReflectingActivity()
    {
        // Set variables here and don't hard code them directly
        _activityDescription = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
        _promptList.AddRange(new List<string> { "Think of a time when you stood up for someone else.", "Think of a time when you did something really difficult.", "Think of a time when you helped someone in need.", "Think of a time when you did something truly selfless." });
        _promptQuestions.AddRange(new List<string> {"Why was this experience meaningful to you?", "Have you ever done anything like this before?", "How did you get started?", "How did you feel when it was complete?", "What made this time different than other times when you were not as successful?", "What is your favorite thing about this experience?", "What could you learn from this experience that applies to other situations?", "What did you learn about yourself through this experience?", "How can you keep this experience in mind in the future?"});
    }

    private void DisplayActivityDescription()
    {
        Console.WriteLine($"\n{_activityDescription}");
    }

    private string GetPrompt()
    {
        Random rand = new Random();
        int index = rand.Next(_promptList.Count);
        string randomPrompt = _promptList[index];
        return randomPrompt;
    }

    private string GetQuestion()
    {
        Random rand = new Random();
        int index = rand.Next(_promptQuestions.Count);
        string randomQuestion = _promptQuestions[index];
        return randomQuestion;
    }

    public void RunReflectingActivity()
    {
        Console.Clear();
        base.WelcomeMessage("2");
        DisplayActivityDescription();
        base.SetCountdown(); // Get countdown time length

        int countdown = base.GetCountdown(); // Return countdown to be used

        Console.WriteLine("Get ready... ");
        base.Animation(5);

        Console.WriteLine("Consider the following prompt:");

        Console.WriteLine($"\n--- {GetPrompt()} ---");

        Console.WriteLine("\nWhen you have something in mind, press enter to continue.");
        Console.ReadLine(); // Wait until user hits enter

        Console.Clear();
        Console.WriteLine("\nNow ponder on each of the following questions as they related to this experience.");
        Console.Write("You may begin in: ");
        for (int i = 5; i > 0; i--) // Countdown for 5 seconds
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
        Console.Clear();

        while (countdown != 0) // Ask questions in 10 second increments
        {
            Console.WriteLine($"> {GetQuestion()}");
            base.Animation(10);
            countdown = countdown - 10;
        }
        base.GoodbyeMessage();
        base.Animation(5);
        Console.WriteLine($"You have completed another {base.GetCountdown()} seconds of the Reflecting Activity.");
        base.Animation(5);
        Console.Clear();
    }



}