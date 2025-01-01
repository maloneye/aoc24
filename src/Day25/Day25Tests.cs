using Xunit.Abstractions;

namespace AOC24.Day25;

public class Day25Tests
{
    private const string ExampleInput = "#####\n.####\n.####\n.####\n.#.#.\n.#...\n.....\n\n#####\n##.##\n.#.##\n...##\n...#.\n...#.\n.....\n\n.....\n#....\n#....\n#...#\n#.#.#\n#.###\n#####\n\n.....\n.....\n#.#..\n###..\n###.#\n###.#\n#####\n\n.....\n.....\n.....\n#....\n#.#..\n#.#.#\n#####";
    private readonly Puzzle25 _puzzle;
    private readonly PuzzleTester<int> _tester;

    private readonly ITestOutputHelper _output;

    public Day25Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle = new(_output);
        _tester = new(_puzzle, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        Puzzle25.Pins[] keys = [new(1,6,4,5,4),new(2,3,1,6,4)];
        Puzzle25.Pins[] locks = [new(6,1,3,2,4),new(5,4,5,1,3), new(4,1,3,1,2)];

        var expected = new Puzzle25.Input(keys, locks);
        // Act
        var actual = Puzzle25.Input.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 10092;

        // Act
        var actual = _puzzle.SolvePartOne(ExampleInput);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const long expected = 875318608908;

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