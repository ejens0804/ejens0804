public class Reference
{
    private string _book;
    private string _chapterOrSection;
    private int _verseStart;
    private int _verseEnd;

    public Reference()
    {
        _book = "3 Nephi";
        _chapterOrSection = "11";
        _verseStart = 10;
        _verseEnd = 11;
    }

    public Reference(string book, string chapter, int verseStart, int verseEnd)
    {
        _book = book;
        _chapterOrSection = chapter;
        _verseStart = verseStart;
        _verseEnd = verseEnd;
    }

    public void DisplayReference()
    {
        Console.Clear();
        if (_verseEnd != 0)
        {
            Console.WriteLine($"\n{_book} {_chapterOrSection}:{_verseStart}-{_verseEnd}");
        }
        else
        {
            Console.WriteLine($"\n{_book} {_chapterOrSection}:{_verseStart}");
        }
    }

    public List<int> IterateThroughVerseRange()
    {
        if (_verseEnd != 0)
        {
            List<int> numbersInVerseRange = new List<int>();
            foreach (int number in Enumerable.Range(_verseStart, _verseEnd))
            {
                numbersInVerseRange.Add(number);
            }
            return numbersInVerseRange;
        }
        else
        {
            List<int> numbersInVerseRange = new List<int>();
            numbersInVerseRange.Add(_verseStart);
            return numbersInVerseRange;
        }
    }
}