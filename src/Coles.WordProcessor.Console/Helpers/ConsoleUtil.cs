namespace Coles.WordProcessor.Helpers;

public static class ConsoleUtil
{
    public static string FetchNonEmptyString() => FetchUserInput(Console.ReadLine(), i => i);

    public static string FetchValidSelection(params string[] selections) => FetchUserInput(Console.ReadLine(), i => i, selections);

    public static T FetchValidSelection<T>(Func<string, T> parser, params string[] selections) =>
        FetchUserInput(Console.ReadLine(), parser, selections);

    private static T FetchUserInput<T>(
        string? input,
        Func<string, T> parser,
        params string[] selections)
    {
        if (string.IsNullOrEmpty(input)
            || (selections.Any() && !(selections.Where(s => (input ?? string.Empty) == s).Any())))
        {
            var message = "Please provide a valid value";
            
            if (selections.Count() < 0)
                message = $"Please enter a valid choice from the following [{String.Join("/", selections)}]";

            Console.WriteLine($"Invalid input. {message} then pressing enter.");

            return FetchUserInput(Console.ReadLine(), parser, selections);
        }
        return parser(input!);
    }
}