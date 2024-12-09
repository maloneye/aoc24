using Xunit.Abstractions;

namespace AOC24.Day9;

public class Day9Tests
{
    private const string ExampleInput = "2333133121414131402";

    private readonly Puzzle9 _puzzle8;
    private readonly PuzzleTester<long> _tester;

    private readonly ITestOutputHelper _output;

    public Day9Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle8 = new();
        _tester = new(_puzzle8, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        IReadOnlyList<int> expectedMemory = [2,3,3,3,1,3,3,1,2,1,4,1,4,1,3,1,4,0,2];

        var expected = new Puzzle9.Input(expectedMemory);

        // Act
        var actual = Puzzle9.Input.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 1928;

        // Act
        var actual = _puzzle8.SolvePartOne(ExampleInput);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 2858;

        // Act
        var actual = _puzzle8.SolvePartTwo(ExampleInput);

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