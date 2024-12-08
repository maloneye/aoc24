using Xunit.Abstractions;

namespace AOC24.Day6;

public class Day5Tests
{
    private const string ExampleInput = "47|53\n97|13\n97|61\n97|47\n75|29\n61|13\n75|53\n29|13\n97|29\n53|29\n61|53\n97|53\n61|29\n47|13\n75|47\n97|75\n47|61\n75|61\n47|29\n75|13\n53|13\n\n75,47,61,53,29\n97,61,53,29,13\n75,29,13\n75,97,47,61,53\n61,13,29\n97,13,75,29,47";

    private readonly Puzzle5 _puzzle5;
    private readonly PuzzleTester<int> _tester;

    private readonly ITestOutputHelper _output;

    public Day5Tests(ITestOutputHelper output)
    {
        _output = output;
        _puzzle5 = new();
        _tester = new(_puzzle5, new InputScraper());
    }

    [Fact]
    public void InputParserTest()
    {
        // Arrange
        var expectedRules = new Dictionary<int, List<int>>
        {
            {47, [53,13,61,29]},
            {97, [13,61,47,29,53,75]},
            {75, [29,53,47,61,13]},
            {61, [13,53,29]},
            {29, [13]},
            {53, [29,13]},
        };

        List<List<int>> expectedUpdates =
        [
            [75,47,61,53,29],
            [97,61,53,29,13],
            [75,29,13],
            [75,97,47,61,53],
            [61,13,29],
            [97,13,75,29,47],
        ];

        var expected = new Puzzle5.Input(expectedRules,expectedUpdates);

        // Act
        var actual = Puzzle5.Input.Parse(ExampleInput);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ExamplePartOne()
    {
        // Arrange
        const int expected = 143;

        // Act
        var actual = _puzzle5.SolvePartOne(ExampleInput);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExamplePartTwo()
    {
        // Arrange
        const int expected = 123;

        // Act
        var actual = _puzzle5.SolvePartTwo(ExampleInput);

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