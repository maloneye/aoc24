using Xunit.Abstractions;

namespace AOC24.Day1;

public class Day1Tests
{
    private const string ExampleInput = "3   4\n4   3\n2   5\n1   3\n3   9\n3   3";

    private readonly Puzzle1 _puzzle;
    private readonly PuzzleTester<int> _tester;

    private readonly ITestOutputHelper _output;

    public Day1Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle = new();
        _tester = new(_puzzle, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        var expected = new Puzzle1.Input(left: [3, 4, 2, 1, 3, 3], right: [4, 3, 5, 3, 9, 3]);

        // Act
        var actual = Puzzle1.Input.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 11;

        // Act
        var actual = _puzzle.SolvePartOne(ExampleInput);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 31;

        // Act
        var actual = _puzzle.SolvePartTwo(ExampleInput);

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