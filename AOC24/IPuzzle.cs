namespace AOC24;

public interface IPuzzle<out TOutput>
{
    TOutput SolvePartOne();

    TOutput SolvePartTwo();
}