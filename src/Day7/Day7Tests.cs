using Xunit.Abstractions;

namespace AOC24.Day7;

public class Day7Tests
{
    private const string ExampleInput = "190: 10 19\n3267: 81 40 27\n83: 17 5\n156: 15 6\n7290: 6 8 6 15\n161011: 16 10 13\n192: 17 8 14\n21037: 9 7 18 13\n292: 11 6 16 20";

    private readonly Puzzle7 _puzzle7;
    private readonly PuzzleTester<long> _tester;

    private readonly ITestOutputHelper _output;

    public Day7Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle7 = new();
        _tester = new(_puzzle7, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        IReadOnlyList<(long, List<int>)> expectedEquations =
        [
            (190, [10, 19]),
            (3267, [81, 40, 27]),
            (83, [17, 5]),
            (156, [15, 6]),
            (7290, [6, 8, 6, 15]),
            (161011, [16, 10, 13]),
            (192, [17, 8, 14]),
            (21037, [9, 7, 18, 13]),
            (292, [11, 6, 16, 20]),
        ];
        
        var expected = new Puzzle7.Input(expectedEquations);

        // Act
        var actual = Puzzle7.Input.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 3749;

        // Act
        var actual = _puzzle7.SolvePartOne(ExampleInput);
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 11387;

        // Act
        var actual = _puzzle7.SolvePartTwo(ExampleInput);

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