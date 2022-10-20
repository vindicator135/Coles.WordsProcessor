using Coles.WordProcessor.Services;

namespace Coles.WordProcessor.UnitTest;

public class ProcessTests
{
    public static IEnumerable<object[]> ReversingData = new []
    {
        new object[]
        {
            "a",
            "a",
        },
        new object[]
        {
            "the quick brown fox jumps over the lazy dog",
            "eht kciuq nworb xof spmuj revo eht yzal god",
        },
        new object[]
        {
            "Phrase with a dot.",
            "esarhP htiw a tod."
        }
    };

    [Theory]
    [MemberData(nameof(ReversingData))]
    public void ReversingTheLettersOfWordsWithinTheSentence_ReturnsTheReverse(string input, string expected)
    {
        // Arrange
        var processor = Processor.New();

        // Act
        var actual = processor.Reverse(input);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> AnagramData = new []
    {
        new object[]
        {
            "mime miem",
            new [] { new [] { "mime", "miem"} }
        },
        new object[]
        {
            "minime mnmiie eiminm",
            new [] { new [] {"minime", "mnmiie", "eiminm"} }
        },
        new object[]
        {
            "minime mnmiie mime miem eiminm",
            new [] 
            {
                new [] { "mime", "miem"},
                new [] { "minime", "mnmiie", "eiminm" }
            }
        }
    };

    [Theory]
    [MemberData(nameof(AnagramData))]
    public void DetectAnagrams_ReturnsAnagrams(string input, IEnumerable<IEnumerable<string>> expected)
    {
        // Arrange
        var processor = Processor.New();

        // Act
        var actualSorted = processor
            .DetectAnagrams(input)
            .SelectMany(_ => _)
            .OrderBy(_ => _);

        var expectedSorted = expected
            .SelectMany(_ => _)
            .OrderBy(_ => _);

        // Assert
        Assert.Equal(expectedSorted, actualSorted);
    }

    public static IEnumerable<object[]> RemoveDuplicateData = new []
    {
        new object[] { "Hello Hello my friend!", "Hello  my friend!" },
        new object[] { "Hello Hello, my friend!", "Hello , my friend!" },
        new object[] { "Wazzup Wazzup, dude ! ! !", "Wazzup , dude !  " }
    };

    [Theory]
    [MemberData(nameof(RemoveDuplicateData))]
    public void RemoveDuplicate_ReturnsNoDuplicateWords(string input, string expected)
    {
        // Arrange
        var processor = Processor.New();

        // Act
        var actual = processor.RemoveRepeatedWords(input);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}