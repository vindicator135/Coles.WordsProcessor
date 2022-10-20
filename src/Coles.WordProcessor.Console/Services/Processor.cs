using Coles.WordProcessor.Interface;
using Coles.WordProcessor.Models;
using Coles.WordProcessor.Helpers;

namespace Coles.WordProcessor.Services;

public class Processor : IOperationProcessor
{
    private readonly char[] _reservedCharacters;
    private StringComparison _stringComparison;
    
    private Processor(
        string reservedCharacters = "!,:.",
        StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
    {
        _reservedCharacters = reservedCharacters.ToCharArray();
        _stringComparison = stringComparison;
    }
    public static Processor New() => new Processor();

    public IEnumerable<IEnumerable<string>> DetectAnagrams(string input)
    {
        return PreProcessInput(input)
            .Select(word => 
            {
                var key = OrderLetters(word);
                return (word, key);
            })
            .GroupBy(wordKey => wordKey.key)
            .Select(wordsByKey => wordsByKey.Select(wordKey => wordKey.word))
            .Where(words => words.Count() > 1);
    }

    private string OrderLetters(string input) => String.Join("", input.ToCharArray().OrderBy(c => c));

    public string RemoveRepeatedWords(string input)
    {
        // 🤔 There's a simple solution:
        //    String.Join(" ", input.Split(" ").Distinct())
        // The major down-side is that it will not remove duplicate words if they're prefixed/suffixed by special characters
        //  i.e. hello hello, => hello hello, ❌
        //       hello hello, => hello        ✅
        // ⚠️ Solved this with tail-recursion, to only replace the actual word and leave the punctuations intact.
        return RemoveRecursively(input.Split(" "), Enumerable.Empty<string>(), Enumerable.Empty<string>());
    }

    string RemoveRecursively(IEnumerable<string> remaining, IEnumerable<string> usedWords, IEnumerable<string> outputCounter)
    { 
        if (remaining.Count() == 0)
            return String.Join(" ", outputCounter);
        
        var fullWord = remaining.First(); 
        var sanitizedWord = Sanitise(remaining.First()).Trim();

        var alreadyUsed = usedWords.Any(usedWord => usedWord == sanitizedWord);
        var output = string.Empty;
        
        if (alreadyUsed)
        {
            output = fullWord.Replace(sanitizedWord, string.Empty);
        }
        else
        {
            output = fullWord;
            usedWords = usedWords.Append(sanitizedWord);
        }

        return RemoveRecursively(remaining.Skip(1), usedWords, outputCounter.Append(output));
    }

    public string Reverse(string input)
    {
        var words = PreProcessInput(input);

        var reversed = input;

        foreach(var word in words)
        {
            string reversedWord = string.Join("", word.ToCharArray().Reverse());
            reversed = reversed.Replace(word, reversedWord);
        }

        return reversed;
    }

    public string Process(Operation operation, string input) => operation switch 
    {
        Operation.ReverseWords => this.Reverse(input),
        Operation.DetectAnagrams => this.DetectAnagrams(input)
            .Select(anagrams => String.Join(",", anagrams))
            .Aggregate(string.Empty, (state, current) => 
            {
                return $"{state}[{current}]";
            })
            .MapFromString(result => string.IsNullOrEmpty(result) ? "There are no anagrams found." : $"{result} have been detected as anagrams."),
        Operation.RemoveRepeated => this.RemoveRepeatedWords(input),
        _ => throw new ArgumentException($"{operation} is not yet supported")
    };

    private IEnumerable<string> PreProcessInput(string input)
    {
        // Sanitise then split into words
        return Sanitise(input).Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }

    private string Sanitise(string input)
    {
        // We want to treat single characters as words.
        // i.e. ! => ! , a => a ✅ 
        //      a! => !         ❌
        if (input.Length == 1)
            return input;
        
        string sanitisedInput = input;

        foreach(var delimiter in _reservedCharacters)
        {
            sanitisedInput = sanitisedInput.Replace(delimiter, ' ');
        }

        return sanitisedInput;
    }
}