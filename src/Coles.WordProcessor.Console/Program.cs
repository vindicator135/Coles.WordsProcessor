using Coles.WordProcessor.Helpers;
using Coles.WordProcessor.Models;
using Coles.WordProcessor.Services;

using static Coles.WordProcessor.Helpers.ConsoleUtil;
using static Coles.WordProcessor.Helpers.Extensions;

namespace Coles.WordProcessor;// Note: actual namespace depends on the project name.


internal class Program
{
    static void Main(string[] args)
    {
        var processor = Processor.New();

        while(true)
        {
            Console.WriteLine("Welcome to the WordProcessor!\n");
            Console.WriteLine("To start, please provide a sentence or a group of words - ");

            var words = FetchNonEmptyString();

            Console.WriteLine($"\nWhat would you like to do with '{words}'?");
            Console.WriteLine(OperationToString(Operation.ReverseWords));
            Console.WriteLine(OperationToString(Operation.DetectAnagrams));
            Console.WriteLine(OperationToString(Operation.RemoveRepeated));
            
            var operation = FetchValidSelection(Extensions.OperationFromString, "1", "2", "3");

            Console.WriteLine($"Applying `{OperationToString(operation)}` to : {words}\n");

            string outcome = processor.Process(operation, words);

            Console.WriteLine($"Results to : {outcome}\n");

            Console.WriteLine("Do you want to try again? [y/n]");
            if (FetchValidSelection("y", "n") == "n")
                return;

            Console.WriteLine("\n\n\n");
        }
    }
}

