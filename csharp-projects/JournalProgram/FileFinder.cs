public class FileFinder
{
    // List<Journal> _fileList = new List<Journal>();
    string[] files;
    string _currentDir;


    public void DisplayFileListOfSavedJournals()
    {
        _currentDir = Directory.GetCurrentDirectory();
        files = Directory.GetFiles(_currentDir, $"*{".csv"}");
        foreach (string file in files)
        {
            Console.WriteLine(Path.GetFileName(file));
        }
    }
}