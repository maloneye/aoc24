using Xunit.Abstractions;

namespace AOC24.DayOne;

public class DayOneTests
{
    private const string InputFilePath = "./Day1/input.txt";
    private const string TestInputFilePath = "./Day1/input-test.txt";

    private readonly ITestOutputHelper _output;

    public DayOneTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        var expected = 11;
        IReadOnlyList<int> first = [3, 4, 2, 1, 3, 3];
        IReadOnlyList<int> second = [4, 3, 5, 3, 9, 3];

        var input = new PuzzleOne.Input(first, second);
        var puzzle = new PuzzleOne(input);

        // Act
        var actual = puzzle.SolvePartOne();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        var expected = 31;
        IReadOnlyList<int> left = [3, 4, 2, 1, 3, 3];
        IReadOnlyList<int> right = [4, 3, 5, 3, 9, 3];

        var input = new PuzzleOne.Input(left, right);
        var puzzle = new PuzzleOne(input);

        // Act
        var actual = puzzle.SolvePartTwo();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        var expected = new PuzzleOne.Input(left: [39472, 41795, 66901, 49097], right: [15292, 28867, 41393, 61173]);

        using var reader = new StreamReader(File.OpenRead(TestInputFilePath));

        // Act
        var actual = PuzzleOne.Input.Parse(reader);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void PartOneTest()
    {
        // Arrange
        using var reader = new StreamReader(File.OpenRead(InputFilePath));
        var input = PuzzleOne.Input.Parse(reader);
        var puzzle = new PuzzleOne(input);

        // Act
        var answer = puzzle.SolvePartOne();

        _output.WriteLine($"Answer: {answer}");
    }

    [Fact]
    public void PartTwoTest()
    {
        // Arrange
        using var reader = new StreamReader(File.OpenRead(InputFilePath));
        var input = PuzzleOne.Input.Parse(reader);
        var puzzle = new PuzzleOne(input);

        // Act
        var answer = puzzle.SolvePartTwo();

        _output.WriteLine($"Answer: {answer}");
    }
}