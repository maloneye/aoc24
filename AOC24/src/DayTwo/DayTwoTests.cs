using AOC24.DayOne;
using Xunit.Abstractions;

namespace AOC24.DayTwo;

public class DayTwoTests
{
    private const string InputFilePath = "./DayTwo/Resources/input.txt";
    private const string TestInputFilePath = "./DayTwo/Resources/input-test.txt";

    private readonly ITestOutputHelper _output;

    public DayTwoTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 2;

        List<List<int>> reports =
        [
            [7, 6, 4, 2, 1],
            [1, 2, 7, 8, 9],
            [9, 7, 6, 2, 1],
            [1, 3, 2, 4, 5],
            [8, 6, 4, 4, 1],
            [1, 3, 6, 7, 9],
        ];

        var input = new PuzzleTwo.Input(reports);
        var puzzle = new PuzzleTwo(input);

        // Act
        var actual = puzzle.SolvePartOne();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 4;

        List<List<int>> reports =
        [
            [7, 6, 4, 2, 1],
            [1, 2, 7, 8, 9],
            [9, 7, 6, 2, 1],
            [1, 3, 2, 4, 5],
            [8, 6, 4, 4, 1],
            [1, 3, 6, 7, 9],
        ];

        var input = new PuzzleTwo.Input(reports);
        var puzzle = new PuzzleTwo(input);

        // Act
        var actual = puzzle.SolvePartTwo();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        List<List<int>> sample =
        [
            [92, 94, 97, 98, 97],
            [26, 27, 28, 31, 33, 34, 37, 37],
            [56, 59, 60, 61, 62, 65, 69],
            [42, 44, 46, 48, 55],
        ];

        var expected = new PuzzleTwo.Input(sample);
        using var reader = new StreamReader(File.OpenRead(TestInputFilePath));
        
        // Act
        var actual = PuzzleTwo.Input.Parse(reader);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void PartOneTest()
    {
        // Arrange
        using var reader = new StreamReader(File.OpenRead(InputFilePath));
        var input = PuzzleTwo.Input.Parse(reader);
        var puzzle = new PuzzleTwo(input);

        // Act
        var answer = puzzle.SolvePartOne();

        _output.WriteLine($"Answer: {answer}");
    }

    [Fact]
    public void PartTwoTest()
    {
    }
}