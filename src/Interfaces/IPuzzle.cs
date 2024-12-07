namespace AOC24;

public interface IPuzzle<out TOutput>
{
    int Day { get; }
    
    TOutput SolvePartOne(string rawInput);
    
    TOutput SolvePartTwo(string rawInput);
}