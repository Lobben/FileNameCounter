using System.Collections.Generic;

namespace FileNameCounter
{
    public interface IFileHandler
    {
        IEnumerable<string> ReadLines(string path);

        string GetFileNameWithoutExtension(string path);
    }
}
