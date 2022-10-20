using Coles.WordProcessor.Models;

namespace Coles.WordProcessor.Helpers;

public static class Extensions
{
    public static Operation OperationFromString(string input)
    {
        Operation operation;
        return Enum.TryParse(input, out operation)
            ? operation
            : throw new ArgumentException($"The {input} is not a Operation");
    }

    public static string OperationToString(Operation op) => op switch
    {
        Operation.ReverseWords => "[1] Reverse the letters of words within the sentence",
        Operation.DetectAnagrams => "[2] Detect if two sets of characters are anagrams",
        Operation.RemoveRepeated => "[3] Remove the repeated elements of an array",
        _ => $"{op}"
    };

    public static T MapFromString<T>(this string v, Func<string, T> map) => map(v);
}