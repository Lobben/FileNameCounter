using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FileNameCounter
{
    public class FileContentCounter : IFileContentCounter
    {
        private readonly IFileHandler _fileHandler;

        public FileContentCounter(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        public int GetOccurrencesInFile(string path, string strToFind)
        {
            var lines = _fileHandler.ReadLines(path);

            var totalOccurences = 0;
            foreach (var line in lines) 
            {
                totalOccurences += GetOccurrencesInString(line, strToFind);
            }

            return totalOccurences;
        }

        private int GetOccurrencesInString(string str, string strToFind)
        {
            var pattern = $"(?={Regex.Escape(strToFind)})";
            return Regex.Matches(str, pattern, RegexOptions.IgnoreCase).Count;
        }
    }
}
