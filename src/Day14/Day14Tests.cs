using Xunit.Abstractions;

namespace AOC24.Day14;

public class Day14Tests
{
    private const string ExampleInput = "p=0,4 v=3,-3\np=6,3 v=-1,-3\np=10,3 v=-1,2\np=2,0 v=2,-1\np=0,0 v=1,3\np=3,0 v=-2,-2\np=7,6 v=-1,-3\np=3,0 v=-1,-2\np=9,3 v=2,3\np=7,3 v=-1,2\np=2,4 v=2,-3\np=9,5 v=-3,-3";
    private readonly Puzzle14 _puzzle;
    private readonly PuzzleTester<int> _tester;

    private readonly ITestOutputHelper _output;

    public Day14Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle = new();
        _tester = new(_puzzle, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        IReadOnlyList<Puzzle14.Robot> robots =
        [
            new(new Position<int>(0, 4), new Position<int>(3, -3)),
            new(new Position<int>(6, 3), new Position<int>(-1, -3)),
            new(new Position<int>(10, 3), new Position<int>(-1, 2)),
            new(new Position<int>(2, 0), new Position<int>(2, -1)),
            new(new Position<int>(0, 0), new Position<int>(1, 3)),
            new(new Position<int>(3, 0), new Position<int>(-2, -2)),
            new(new Position<int>(7, 6), new Position<int>(-1, -3)),
            new(new Position<int>(3, 0), new Position<int>(-1, -2)),
            new(new Position<int>(9, 3), new Position<int>(2, 3)),
            new(new Position<int>(7, 3), new Position<int>(-1, 2)),
            new(new Position<int>(2, 4), new Position<int>(2, -3)),
            new(new Position<int>(9, 5), new Position<int>(-3, -3))
        ];

        var expected = new Puzzle14.Input(robots);

        // Act
        var actual = Puzzle14.Input.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 12;
        _puzzle.SetBounds(11,7);
        // Act
        var actual = _puzzle.SolvePartOne(ExampleInput);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task PartOneTest()
    {
        _puzzle.SetBounds(101,103);
        var answer = await _tester.SolvePartOne();
        
        _output.WriteLine($"Answer: {answer}");
    }

    [Fact]
    public async Task PartTwoTest()
    {
        _puzzle.SetBounds(101,103);
        var answer = await _tester.SolvePartTwo();

        _output.WriteLine($"Answer: {answer}");
    }
}