using Xunit.Abstractions;

namespace AOC24.DayTwo;

public class DayThreeTests
{
    const string ExampleInput = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

    private readonly PuzzleThree _puzzle;
    private readonly PuzzleTester<int> _tester;

    private readonly ITestOutputHelper _output;

    public DayThreeTests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle = new();
        _tester = new(_puzzle, new InputScraper());
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 161;

        // Act
        var actual = _puzzle.SolvePartOne(ExampleInput);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 4;

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