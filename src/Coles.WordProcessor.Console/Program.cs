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
                Console.WriteLine("To start, please provide a sentence or a group of words - ");

                var words = Console.ReadLine();

                Console.WriteLine($"What would you like to do with 'words'?");
                Console.WriteLine(" (1) Reverse the letters of words within the sentence");
                Console.WriteLine(" (2) Detect if two sets of characters are anagrams");
                Console.WriteLine(" (3) Remove the repeated elements of an array");
                
                var operation = Console.ReadLine();

                Console.WriteLine($"Applying ({operation}) to : '{words}'");

                string outcome = Process(operation, words);

                Console.WriteLine($"Results to : {outcome}");
                Console.WriteLine("Do you want to try again? (y/n)");

                var tryAgain = Console.ReadLine();
                
                if (!StartAgain(tryAgain))
                    return;
            }
        }

        private static string Process(string? operation, string? words)
        {
            return "Yipee!";
            
        }

        private static bool StartAgain(string? tryAgain)
        {
            return true;
        }
    }
}

