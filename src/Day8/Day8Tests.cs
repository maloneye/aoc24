using Xunit.Abstractions;

namespace AOC24.Day8;

public class Day8Tests
{
    private const string ExampleInput = "............\n........0...\n.....0......\n.......0....\n....0.......\n......A.....\n............\n............\n........A...\n.........A..\n............\n............";

    private readonly Puzzle8 _puzzle8;
    private readonly PuzzleTester<long> _tester;

    private readonly ITestOutputHelper _output;

    public Day8Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle8 = new(output);
        _tester = new(_puzzle8, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        char[,] expectedMap = new[,]
        {
            { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '0', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '0', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '0', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '0', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', 'A', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.', 'A', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.', '.', 'A', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.' },
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
        _puzzle8.IsRenderingEnabled = true;
        const int expected = 14;

        // Act
        var actual = _puzzle8.SolvePartOne(ExampleInput);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        _puzzle8.IsRenderingEnabled = true;
        const int expected = 34;

        // Act
        var actual = _puzzle8.SolvePartTwo(ExampleInput);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task PartOneTest()
    {
        _puzzle8.IsRenderingEnabled = true;
        var answer = await _tester.SolvePartOne();

        _output.WriteLine($"Answer: {answer}");
    }

    [Fact]
    public async Task PartTwoTest()
    {
        _puzzle8.IsRenderingEnabled = true;
        var answer = await _tester.SolvePartTwo();

        _output.WriteLine($"Answer: {answer}");
    }
}