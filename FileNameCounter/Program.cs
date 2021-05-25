using FileNameCounter;
using System;
using System.IO;
using System.Security;

namespace ConsoleApp1
{
    public class Program
    {
        private readonly string[] _args;
        private readonly IFileContentCounter _fileContentCounter;
        private readonly IFileHandler _fileHandler;

        Program(string[] args, IFileHandler fileHandler, IFileContentCounter fileContentCounter)
        {
            _args = args;
            _fileHandler = fileHandler;
            _fileContentCounter = fileContentCounter;
        }


        private void Run()
        {
            if (_args.Length != 1)
            {
                Console.Error.WriteLine("Expected exactly one argument");
                return;
            }

            var path = _args[0];
            var fileName = GetFileNameOrLogError(path);

            if (fileName == null)
                return;

            var occurences = GetOccurencesOrLogError(path, fileName);
            if (occurences == null)
                return;

            Console.WriteLine("Found " + occurences);
        }

        private string GetFileNameOrLogError(string path)
        {
            string fileName = null;
            try
            {
                fileName = _fileHandler.GetFileNameWithoutExtension(path);
            }
            catch(ArgumentException)
            {
                Console.Error.WriteLine("Invalid file path");
            }

            return fileName;
        }

        private int? GetOccurencesOrLogError(string path, string stringToLookFor)
        {
            int? occurences = null;
            try
            {
                occurences = _fileContentCounter.GetOccurrencesInFile(path, stringToLookFor);
            }
            catch (Exception e) when (e is ArgumentException || e is ArgumentNullException)
            {
                Console.Error.WriteLine("Invalid file path");
            }
            catch (Exception e) when(e is DirectoryNotFoundException || e is FileNotFoundException)
            {
                Console.Error.WriteLine("File could not be found");
            }
            catch (Exception e) when (e is SecurityException || e is UnauthorizedAccessException)
            {
                Console.Error.WriteLine("You don't have permission to read the file");
            }
            catch (PathTooLongException)
            {
                Console.Error.WriteLine("File path is to long");
            }
            catch (IOException e)
            {
                Console.Error.WriteLine($"IO Exception. Message: {e.Message}");
            }

            return occurences;
        }

        static void Main(string[] args)
        {
            var fileHandler = new FileHandler();
            var fileContentCounter = new FileContentCounter(fileHandler);

            Program program = new Program(args, fileHandler, fileContentCounter);
            program.Run();
        }
    }
}