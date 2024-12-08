using Xunit.Abstractions;

namespace AOC24.Day6;

public class Day6Tests
{
    private const string ExampleInput = "....#.....\n.........#\n..........\n..#.......\n.......#..\n..........\n.#..^.....\n........#.\n#.........\n......#...";

    private readonly Puzzle6 _puzzle6;
    private readonly PuzzleTester<int> _tester;

    private readonly ITestOutputHelper _output;

    public Day6Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle6 = new(output);
        _tester = new(_puzzle6, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        var expectedMap = new char[,]
        {
            { '.', '.', '.', '.', '#', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.', '.', '#' },
            { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '#', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '#', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '#', '.', '.', '^', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.', '#', '.' },
            { '#', '.', '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '#', '.', '.', '.' },
        };
        
        var expected = new Puzzle6.Input(expectedMap);

        // Act
        var actual = Puzzle6.Input.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 41;

        // Act
        var actual = _puzzle6.SolvePartOne(ExampleInput);
        
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        _puzzle6.IsRenderingEnabled = true;
        const int expected = 6;

        // Act
        var actual = _puzzle6.SolvePartTwo(ExampleInput);

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
        _puzzle6.IsRenderingEnabled = false;

        var answer = await _tester.SolvePartTwo();

        _output.WriteLine($"Answer: {answer}");
    }
}