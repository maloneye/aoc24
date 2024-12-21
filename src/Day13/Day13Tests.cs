using Xunit.Abstractions;

namespace AOC24.Day13;

public class Day13Tests
{
    private const string ExampleInput = "Button A: X+94, Y+34\nButton B: X+22, Y+67\nPrize: X=8400, Y=5400\n\nButton A: X+26, Y+66\nButton B: X+67, Y+21\nPrize: X=12748, Y=12176\n\nButton A: X+17, Y+86\nButton B: X+84, Y+37\nPrize: X=7870, Y=6450\n\nButton A: X+69, Y+23\nButton B: X+27, Y+71\nPrize: X=18641, Y=10279\n";

    private readonly Puzzle13 _puzzle;
    private readonly PuzzleTester<long> _tester;

    private readonly ITestOutputHelper _output;

    public Day13Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle = new(output);
        _tester = new(_puzzle, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
       List<Puzzle13.Run> runs = 
       [
           new(new(94, 34),
               new(22, 67),
               new(8400, 5400)
           ),
           new(new(26, 66),
               new(67, 21),
               new(12748, 12176)
           ),
           new(new(17, 86),
               new(84, 37),
               new(7870, 6450)
           ),
           new(new(69, 23),
               new(27, 71),
               new(18641, 10279)
           )
       ];
        
        var expected = new Puzzle13.Input(runs);

        // Act
        var actual = Puzzle13.Input.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 480;

        // Act
        var actual = _puzzle.SolvePartOne(ExampleInput);
       
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 55312;

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