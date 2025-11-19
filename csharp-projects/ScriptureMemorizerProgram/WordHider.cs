public class WordHider
{
    public WordHider() { }

    public List<string> PickWordsToHide(List<string> wordsToChooseFrom, int count = 10)
    {
        if (wordsToChooseFrom.Count == 0)
            return new List<string>();

        Random rndm = new Random();
        int wordsToPick = Math.Min(count, wordsToChooseFrom.Count);
        List<string> availableWords = new List<string>(wordsToChooseFrom);
        List<string> pickedWords = new List<string>();

        for (int i = 0; i < wordsToPick; i++)
        {
            int index = rndm.Next(availableWords.Count);
            pickedWords.Add(availableWords[index]);
            availableWords.RemoveAt(index);
        }
        return pickedWords;
    }
}