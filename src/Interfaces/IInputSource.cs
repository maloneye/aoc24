namespace AOC24;

public interface IInputSource
{
    ValueTask<string> GetInput(int day, CancellationToken token = default);
}