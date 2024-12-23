using Xunit.Abstractions;

namespace AOC24.Day15;

public class Day15Tests
{
    private const string ExampleInput =
        "##########\n#..O..O.O#\n#......O.#\n#.OO..O.O#\n#..O@..O.#\n#O#..O...#\n#O..O..O.#\n#.OO.O.OO#\n#....O...#\n##########\n\n<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^\nvvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v\n><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<\n<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^\n^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><\n^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^\n>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^\n<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>\n^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>\nv^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^\n";

    private readonly Puzzle15 _puzzle;
    private readonly PuzzleTester<int> _tester;

    private readonly ITestOutputHelper _output;

    public Day15Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle = new(_output);
        _tester = new(_puzzle, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        char[,] map = new char[,]
        {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', '.', '.', 'O', '.', '.', 'O', '.', 'O', '#' },
            { '#', '.', '.', '.', '.', '.', '.', 'O', '.', '#' },
            { '#', '.', 'O', 'O', '.', '.', 'O', '.', 'O', '#' },
            { '#', '.', '.', 'O', '@', '.', '.', 'O', '.', '#' },
            { '#', 'O', '#', '.', '.', 'O', '.', '.', '.', '#' },
            { '#', 'O', '.', '.', 'O', '.', '.', 'O', '.', '#' },
            { '#', '.', 'O', 'O', '.', 'O', '.', 'O', 'O', '#' },
            { '#', '.', '.', '.', '.', 'O', '.', '.', '.', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' }
        };

        // It's ok I get claude to do all these i'm not that sad
        Puzzle15.Direction[] directions = [Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Up, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Up, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Up, Puzzle15.Direction.Up, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Up, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Up, Puzzle15.Direction.Right, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Down, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Down, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Down, Puzzle15.Direction.Right, Puzzle15.Direction.Up, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Left, Puzzle15.Direction.Up];

        var expected = new Puzzle15.Input(directions, map);
        // Act
        var actual = Puzzle15.Input.Parse(ExampleInput);

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