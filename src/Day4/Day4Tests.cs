using Xunit.Abstractions;

namespace AOC24.Day4;

public class Day4Tests
{
    private const string ExampleInput = "MMMSXXMASM\nMSAMXMSMSA\nAMXSXMAAMM\nMSAMASMSMX\nXMASAMXAMM\nXXAMMXXAMA\nSMSMSASXSS\nSAXAMASAAA\nMAMMMXMMMM\nMXMXAXMASX";
    
    private readonly Puzzle4 _puzzle4;
    private readonly PuzzleTester<int> _tester;

    private readonly ITestOutputHelper _output;

    public Day4Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle4 = new();
        _tester = new(_puzzle4, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        var expectedCrossword =  new char[,]
        {
            { 'M', 'M', 'M', 'S', 'X', 'X', 'M', 'A', 'S', 'M' },
            { 'M', 'S', 'A', 'M', 'X', 'M', 'S', 'M', 'S', 'A' },
            { 'A', 'M', 'X', 'S', 'X', 'M', 'A', 'A', 'M', 'M' },
            { 'M', 'S', 'A', 'M', 'A', 'S', 'M', 'S', 'M', 'X' },
            { 'X', 'M', 'A', 'S', 'A', 'M', 'X', 'A', 'M', 'M' },
            { 'X', 'X', 'A', 'M', 'M', 'X', 'X', 'A', 'M', 'A' },
            { 'S', 'M', 'S', 'M', 'S', 'A', 'S', 'X', 'S', 'S' },
            { 'S', 'A', 'X', 'A', 'M', 'A', 'S', 'A', 'A', 'A' },
            { 'M', 'A', 'M', 'M', 'M', 'X', 'M', 'M', 'M', 'M' },
            { 'M', 'X', 'M', 'X', 'A', 'X', 'M', 'A', 'S', 'X' },
        };
        
        var expected = new Puzzle4.Input(expectedCrossword);

        // Act
        var actual = Puzzle4.Input.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 18;

        // Act
        var actual = _puzzle4.SolvePartOne(ExampleInput);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 9;

        // Act
        var actual = _puzzle4.SolvePartTwo(ExampleInput);

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