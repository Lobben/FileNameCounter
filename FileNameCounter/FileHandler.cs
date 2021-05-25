using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileNameCounter
{
    public class FileHandler : IFileHandler
    {
        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public IEnumerable<string> ReadLines(string path)
        {
            return File.ReadLines(path, Encoding.UTF8);
        }
    }
}
