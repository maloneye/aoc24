using Xunit.Abstractions;

namespace AOC24.Day11;

public class Day11Tests
{
    private const string ExampleInput = "125 17";

    private readonly Puzzle11 _puzzle11;
    private readonly PuzzleTester<long> _tester;

    private readonly ITestOutputHelper _output;

    public Day11Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle11 = new(output);
        _tester = new(_puzzle11, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        long[] expectedMap = [125, 17];
        
        var expected = new Puzzle11.Input(expectedMap);

        // Act
        var actual = Puzzle11.Input.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 55312;

        // Act
        var actual = _puzzle11.SolvePartOne(ExampleInput);
       
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 55312;

        // Act
        var actual = _puzzle11.SolvePartTwo(ExampleInput);

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