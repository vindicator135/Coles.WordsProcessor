namespace Coles.WordProcessor.Interface;

public interface IOperationProcessor
{
    string Reverse(string input);
    IEnumerable<IEnumerable<string>> DetectAnagrams(string input);

    string RemoveRepeatedWords(string input);
}