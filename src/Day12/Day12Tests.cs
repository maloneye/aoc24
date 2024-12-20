using Xunit.Abstractions;

namespace AOC24.Day12;

public class Day12Tests
{
    private const string ExampleInput = "RRRRIICCFF\nRRRRIICCCF\nVVRRRCCFFF\nVVRCCCJFFF\nVVVVCJJCFE\nVVIVCCJJEE\nVVIIICJJEE\nMIIIIIJJEE\nMIIISIJEEE\nMMMISSJEEE";

    private readonly Puzzle12 _puzzle12;
    private readonly PuzzleTester<long> _tester;

    private readonly ITestOutputHelper _output;

    public Day12Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle12 = new(output);
        _tester = new(_puzzle12, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        char[,] expectedMap = new char[,]
        {
            {'R','R','R','R','I','I','C','C','F','F'},
            {'R','R','R','R','I','I','C','C','C','F'},
            {'V','V','R','R','R','C','C','F','F','F'},
            {'V','V','R','C','C','C','J','F','F','F'},
            {'V','V','V','V','C','J','J','C','F','E'},
            {'V','V','I','V','C','C','J','J','E','E'},
            {'V','V','I','I','I','C','J','J','E','E'},
            {'M','I','I','I','I','I','J','J','E','E'},
            {'M','I','I','I','S','I','J','E','E','E'},
            {'M','M','M','I','S','S','J','E','E','E'}
        };
        
        var expected = new CharMapInput(expectedMap);

        // Act
        var actual = CharMapInput.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 1930;

        // Act
        var actual = _puzzle12.SolvePartOne(ExampleInput);
       
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 55312;

        // Act
        var actual = _puzzle12.SolvePartTwo(ExampleInput);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task PartOneTest()
    {
        var answer = await _tester.SolvePartOne();

        _output.WriteLine($"Answer: {answer}");
    }

    [Fact]
    public async Task PartTwoTest()
    {
        var answer = await _tester.SolvePartTwo();

        _output.WriteLine($"Answer: {answer}");
    }
}