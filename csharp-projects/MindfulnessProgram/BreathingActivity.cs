public class BreathingActivity : Activity
{
    private string _activityDescription;
    private string _breatheInPrompt;
    private string _breatheOutPrompt;

    public BreathingActivity()
    {
        // set variables here and don't hard code them directly
        _activityDescription = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
        _breatheInPrompt = "Breathe in... ";
        _breatheOutPrompt = "Breathe out... ";
    }

    private void DisplayActivityDescription()
    {
        Console.WriteLine($"\n{_activityDescription}");
    }

    public void RunBreathingActivity()
    {
        
        Console.Clear();
        base.WelcomeMessage("1");

        DisplayActivityDescription();

        int countdown = base.SetCountdown();
        Console.CursorVisible = false;
        Console.WriteLine("Get ready...");
        base.Animation(5);

        while (countdown > 0)
        {
            Console.Write($"\n{_breatheInPrompt}");
            for (int i = 4; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
            countdown = countdown - 4;


            Console.Write($"\n{_breatheOutPrompt}");
            for (int i = 6; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
            countdown = countdown - 6;
            Console.WriteLine();
            Console.WriteLine();
        }
        base.GoodbyeMessage();
        base.Animation(5);
        Console.WriteLine($"You have completed another {base.GetCountdown()} seconds of the Breathing Activity.");
        base.Animation(5);
        Console.Clear();
        Console.CursorVisible = true;
    }
}