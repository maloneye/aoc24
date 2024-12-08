using Xunit.Abstractions;

namespace AOC24.Day2;

public class Day3Tests
{
    const string ExampleInputOne = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
    const string ExampleInputTwo = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
   
    private readonly Puzzle3 _puzzle;
    private readonly PuzzleTester<int> _tester;

    private readonly ITestOutputHelper _output;

    public Day3Tests(ITestOutputHelper output)
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
        var actual = _puzzle.SolvePartOne(ExampleInputOne);

        // Assert
        Assert.Equal(expected, actual);
    }
   
    [Fact]
    public void EdgeCase1PartOne()
    {
        // Arrange
        const int expected = 200_000_000;
        const string input = "mmul(10000,20000)";

        // Act
        var actual = _puzzle.SolvePartOne(input);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 48;

        // Act
        var actual = _puzzle.SolvePartTwo(ExampleInputTwo);

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