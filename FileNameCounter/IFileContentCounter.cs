namespace FileNameCounter
{
    public interface IFileContentCounter
    {
        int GetOccurrencesInFile(string path, string stringToFind);
    }
}