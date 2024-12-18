using Xunit.Abstractions;

namespace AOC24.Day10;

public class Day10Tests
{
    private const string ExampleInput = "89010123\n78121874\n87430965\n96549874\n45678903\n32019012\n01329801\n10456732";

    private readonly Puzzle10 _puzzle10;
    private readonly PuzzleTester<long> _tester;

    private readonly ITestOutputHelper _output;

    public Day10Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle10 = new(output);
        _tester = new(_puzzle10, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        int[,] expectedMap = new int[,]
        {
            {8, 9, 0, 1, 0, 1, 2, 3},
            {7, 8, 1, 2, 1, 8, 7, 4},
            {8, 7, 4, 3, 0, 9, 6, 5},
            {9, 6, 5, 4, 9, 8, 7, 4},
            {4, 5, 6, 7, 8, 9, 0, 3},
            {3, 2, 0, 1, 9, 0, 1, 2},
            {0, 1, 3, 2, 9, 8, 0, 1},
            {1, 0, 4, 5, 6, 7, 3, 2}
        };
        
        var expected = new MapInput(expectedMap);

        // Act
        var actual = MapInput.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 36;

        // Act
        var actual = _puzzle10.SolvePartOne(ExampleInput);
       
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 81;

        // Act
        var actual = _puzzle10.SolvePartTwo(ExampleInput);

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