using System;
using System.Text;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the WordProcessor!");
            while(true)
            {
                Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}");
                Console.WriteLine("To start, please provide a sentence or a group of words - ");

                var words = Console.ReadLine();

                Console.WriteLine($"What would you like to do with '{words}'?");
                Console.WriteLine(" (1) Reverse the letters of words within the sentence");
                Console.WriteLine(" (2) Detect if two sets of characters are anagrams");
                Console.WriteLine(" (3) Remove the repeated elements of an array");
                
                var operation = ExtractSelection(Console.ReadLine(), "1", "2", "3");

                Console.WriteLine($"Applying ({operation}) to : '{words}'");

                string outcome = Process(operation, words);

                Console.WriteLine($"Results to : {outcome}");
                Console.WriteLine("Do you want to try again? (y/n)");

                var tryAgain = ExtractSelection(Console.ReadLine(), "y", "n");
                
                if (!StartAgain(tryAgain))
                    return;
            }
        }

        private static string Process(string? operation, string? words)
        {
            return "Yipee!";
        }

        private static bool StartAgain(string yesNo)
        {
            return yesNo == "y";
        }

        private static string ExtractSelection(string? input, params string[] selections)
        {
            if (input == null 
                || (!(selections.Where(s => (input ?? string.Empty) == s).Any())))
            {
                
                Console.WriteLine($"Please enter a valid choice from the following [{String.Join("/", selections)}] then pressing enter.");

                return ExtractSelection(Console.ReadLine(), selections);
            }
            return input!;
        }
    }
}

