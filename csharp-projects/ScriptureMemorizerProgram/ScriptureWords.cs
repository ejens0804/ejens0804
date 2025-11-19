using System.Text.RegularExpressions;

public class ScriptureWords
{

    private Dictionary<int, string> _originalScriptureVerseWordsDict;
    private Dictionary<int, string> _scriptureVerseWordsDict = new Dictionary<int, string>();
    private List<string> _scriptureIndividualWordsList = new List<string>();
    private List<string> _hiddenWords = new List<string>();

    public ScriptureWords()
    {
        _scriptureVerseWordsDict[1] = "Behold, I am Jesus Christ, whom the prophets testified shall come into the world.";
        _scriptureVerseWordsDict[2] = "And behold, I am the light and the life of the world; and I have drunk out of that bitter cup which the Father hath given me, and have glorified the Father in taking upon me the sins of the world, in the which I have suffered the will of the Father in all things from the beginning.";
    }

    public ScriptureWords(int numberOfVerses)
    {
        string scriptureWords;
        for (int number = 0; number < numberOfVerses; number++)
        {
            Console.WriteLine("Please put the words for the next verse to enter below:");
            scriptureWords = Console.ReadLine();
            _scriptureVerseWordsDict.Add(number, scriptureWords);
        }
    }
    
    public List<string> CombineScriptureDictionaryWords()
    {
        _scriptureIndividualWordsList.Clear();

        foreach (var val in _scriptureVerseWordsDict.Values)
        {
            if (string.IsNullOrEmpty(val))
            {
                continue;
            }
            var words = val.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                string cleanWord = Regex.Replace(word, @"[^\w]", "").ToLowerInvariant();
                if (!string.IsNullOrEmpty(cleanWord))
                {
                    _scriptureIndividualWordsList.Add(cleanWord);
                }
            }
        }
        return _scriptureIndividualWordsList;
    }


    private void AddNumbersToVerses()
    {
        List<int> verseNumbers;
        Reference ref1 = new Reference();

        verseNumbers = ref1.IterateThroughVerseRange();
        int verseNumbersCount = 0;
        var keysToUpdate = new List<(int oldKey, int newKey, string value)>();
        foreach (var dictLine in _scriptureVerseWordsDict)
        {
            int oldKey = dictLine.Key;
            string value = dictLine.Value;
            int newKey = verseNumbers[verseNumbersCount];
            verseNumbersCount += 1;
            keysToUpdate.Add((oldKey, newKey, value));
        }

        foreach (var (oldKey, newKey, value) in keysToUpdate)
        {
            _scriptureVerseWordsDict.Remove(oldKey);
            _scriptureVerseWordsDict[newKey] = value;
        }
    }

    private void AddNumbersToVerses(string book, string chapter, int verseStart, int verseEnd)
    {
        List<int> verseNumbers;
        Reference ref1 = new Reference(book, chapter, verseStart, verseEnd);

        verseNumbers = ref1.IterateThroughVerseRange();
        int verseNumbersCount = 0;
        var keysToUpdate = new List<(int oldKey, int newKey, string value)>();
        foreach (var dictLine in _scriptureVerseWordsDict)
        {
            int oldKey = dictLine.Key;
            string value = dictLine.Value;
            int newKey = verseNumbers[verseNumbersCount];
            verseNumbersCount += 1;
            keysToUpdate.Add((oldKey, newKey, value));
        }

        foreach (var (oldKey, newKey, value) in keysToUpdate)
        {
            _scriptureVerseWordsDict.Remove(oldKey);
            _scriptureVerseWordsDict[newKey] = value;
        }
    }

    public void DisplayAllWordsInScripture()
    {
        AddNumbersToVerses();
        foreach (KeyValuePair<int, string> dictLine in _scriptureVerseWordsDict)
        {
            Console.WriteLine($"{dictLine.Key} {dictLine.Value}");
        }
    }

    public void DisplayAllWordsInUserScripture(string book, string chapter, int verseStart, int verseEnd)
    {
        AddNumbersToVerses(book, chapter, verseStart, verseEnd);
        foreach (KeyValuePair<int, string> dictLine in _scriptureVerseWordsDict)
        {
            Console.WriteLine($"{dictLine.Key} {dictLine.Value}");
        }
    }

    public void DisplayRemainingWordsInScripture(List<string> wordsToHide)
    {
        _originalScriptureVerseWordsDict = _scriptureVerseWordsDict;
        _hiddenWords.AddRange(wordsToHide);

        foreach (var key in _originalScriptureVerseWordsDict.Keys)
        {
            string original = _originalScriptureVerseWordsDict[key];

            foreach (string word in _hiddenWords)
            {
                // string pattern = $@"\b{Regex.Escape(word.Trim())}(?=\W|\b)";
                string pattern = $@"\b{Regex.Escape(word)}\b";
                string hiddenWord = new string('_', word.Length);
                original = Regex.Replace(original, pattern, hiddenWord, RegexOptions.IgnoreCase);
            }

            _scriptureVerseWordsDict[key] = original;
        }
        AddNumbersToVerses();
        foreach (KeyValuePair<int, string> dictLine in _scriptureVerseWordsDict)
        {
            Console.WriteLine($"{dictLine.Key} {dictLine.Value}");
        }
    }

    public void DisplayRemainingWordsInUserScripture(List<string> wordsToHide, string book, string chapter, int verseStart, int verseEnd)
    {
        _originalScriptureVerseWordsDict = _scriptureVerseWordsDict;
        _hiddenWords.AddRange(wordsToHide);

        foreach (var key in _originalScriptureVerseWordsDict.Keys)
        {
            string original = _originalScriptureVerseWordsDict[key];

            foreach (string word in _hiddenWords)
            {
                string pattern = $@"\b{Regex.Escape(word)}\b";
                string hiddenWord = new string('_', word.Length);
                original = Regex.Replace(original, pattern, hiddenWord, RegexOptions.IgnoreCase);
            }

            _scriptureVerseWordsDict[key] = original;
        }
        AddNumbersToVerses(book, chapter, verseStart, verseEnd);
        foreach (KeyValuePair<int, string> dictLine in _scriptureVerseWordsDict)
        {
            Console.WriteLine($"{dictLine.Key} {dictLine.Value}");
        }
    }

    public bool CheckIfAllWordsAreHidden()
    {
        foreach (var dictLine in _scriptureVerseWordsDict)
        {
            string value = dictLine.Value;

            if (value.Any(c => char.IsLetterOrDigit(c) && c != '_'))
            {
                return false;
            }
        }
        return true;
    }

}