public class PromptGenerator
{
    public List<string> _promptList = new List<string> { "Who was the most interesting person I interacted with today?", "What was the best part of my day?", "How did I see the hand of the Lord in my life today?", "What was the strongest emotion I felt today?", "If I had one thing I could do over today, what would it be?" };

    public string GeneratePrompt()
    {
        Random randomPrompt = new Random();
        string promptUsed = _promptList[randomPrompt.Next(0, _promptList.Count)];
        Console.WriteLine($"\nYour prompt is:\n{promptUsed}");
        // .Next is part of the Random class and selects a random integer between a given range
        // in this case the given range is 0 and the number of items in the list<> _promptList
        return promptUsed;
    }
}