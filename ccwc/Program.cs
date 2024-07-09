
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Parse command line arguments
        bool countBytes = args.Contains("-c");
        bool countLines = args.Contains("-l");
        bool countWords = args.Contains("-w");
        bool countChars = args.Contains("-m");
        
        // Get the filename if provided
        string filename = "ccwc/" + args.LastOrDefault(arg => !arg.StartsWith("-"));

        // Determine input source
        TextReader inputReader;

        if (filename != null)
        {
            // Read from the file
            inputReader = new StreamReader(filename);
        }
        else if (Console.IsInputRedirected)
        {
            // Read from standard input
            inputReader = Console.In;
        }
        else
        {
            Console.WriteLine("Usage: ccwc [-c|-l|-w|-m] [filename]");
            return;
        }

        // Read the content
        string content = inputReader.ReadToEnd();
        
        // Perform counts based on the provided options
        if (countBytes)
        {
            int byteCount = System.Text.Encoding.UTF8.GetByteCount(content);
            Console.WriteLine($"{byteCount} {filename ?? "stdin"}");
        }
        else if (countLines)
        {
            int lineCount = content.Split('\n').Length;
            Console.WriteLine($"{lineCount} {filename ?? "stdin"}");
        }
        else if (countWords)
        {
            int wordCount = content.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Length;
            Console.WriteLine($"{wordCount} {filename ?? "stdin"}");
        }
        else if (countChars)
        {
            int charCount = content.Length;
            Console.WriteLine($"{charCount} {filename ?? "stdin"}");
        }
        else
        {
            // Default case: print lines, words, and bytes
            int lineCount = content.Split('\n').Length;
            int wordCount = content.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Length;
            int byteCount = System.Text.Encoding.UTF8.GetByteCount(content);
            Console.WriteLine($"{lineCount} {wordCount} {byteCount} {filename ?? "stdin"}");
        }

        // Close the file if it's not stdin
        if (inputReader != Console.In)
        {
            inputReader.Dispose();
        }
    }
}
