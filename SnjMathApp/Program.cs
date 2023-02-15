using System.Reflection.PortableExecutable;

namespace SnjMathApp
{
    internal class Program
    {
        private static string? _FilePath;
        private static MathFileProcessor _Processor;
        static Program()
        {
            _Processor = new MathFileProcessor();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Snj!");
            if (InitApp(args))
            {
                Console.WriteLine($"File: {_FilePath}\nProcessing...");
                var responce = _Processor.ProcessFile(_FilePath);

                Console.WriteLine("Processed.");
                DisplayResponce(responce);
            }

            Console.WriteLine("\nDONE.");
            Console.ReadLine();
        }

        private static bool InitApp(string[] args)
        {
            _FilePath = 
                args.Length == 0 
                    ? AskAboutFile() 
                    : args[0];

            if (!File.Exists(_FilePath))
            {
                Console.WriteLine($"File {_FilePath} does not exists.");
                return false;
            }

            // Inited successfull
            return true;
        }

        private static string? AskAboutFile()
        {
            Console.WriteLine("Select file for processing:");
            return Console.ReadLine();
        }

        private static void DisplayResponce(ProcessorResponce responce)
        {
            Console.WriteLine($"\nResponce for: {responce.FilePath}");
            
            Console.WriteLine($"\nLines processed: {responce.LinesCount};");

            Console.WriteLine($"\nBroken lines ({responce.BrokenLines.Count()}):");
            foreach (var b in responce.BrokenLines)
                Console.WriteLine($"\t{b.ToString()} // Error={b.Error.Message}");

            Console.WriteLine($"\nMax sum is {responce.MaxLine.Sum} in line:");
            Console.WriteLine(responce.MaxLine.ToString());
        }
    }
}