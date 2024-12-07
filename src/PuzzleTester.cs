namespace AOC24;

public class PuzzleTester<T>
{
    private readonly IPuzzle<T> _puzzle;
    private readonly IInputSource _inputSource;
    
    public PuzzleTester(IPuzzle<T> puzzle, IInputSource inputSource)
    {
        _inputSource = inputSource;
        _puzzle = puzzle;
    }
    
    public async Task<T> SolvePartOne()
    {
        var rawInput = await _inputSource.GetInput(_puzzle.Day);
        var answer = _puzzle.SolvePartOne(rawInput);

        return answer;
    }
    
    public async Task<T> SolvePartTwo()
    {
        var rawInput = await _inputSource.GetInput(_puzzle.Day);
        var answer = _puzzle.SolvePartTwo(rawInput);

        return answer;
    }
}