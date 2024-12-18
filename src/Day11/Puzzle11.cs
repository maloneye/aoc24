using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Transactions;
using Xunit.Abstractions;

namespace AOC24.Day11;

public class Puzzle11 : IPuzzle<long>
{
    public class Input(IEnumerable<long> stones)
    {
        public IReadOnlyList<long> Stones { get; } = stones.ToList();

        public static Input Parse(string rawInput)
        {
            var stones = rawInput.Split(' ').Select(long.Parse);

            return new Input(stones);
        }
    }

    public int Day { get; } = 11;
    public bool IsRenderingEnabled { get; set; }

    private const string StoneCache = "cache/stones";
    private const string UpdatedCache = "cache/updated";

    private readonly ITestOutputHelper _output;

    public Puzzle11(ITestOutputHelper output)
    {
        _output = output;
    }

    public long SolvePartOne(string rawInput)
    {
        var input = Input.Parse(rawInput);

        var stones = input.Stones;
        for (var i = 0; i < 25; i++)
        {
            stones = Blink(stones);
        }

        return stones.Count;
    }

    public long SolvePartTwo(string rawInput)
    {
        if (File.Exists(UpdatedCache))
        {
            File.Delete(UpdatedCache);
        }

        if (File.Exists(StoneCache))
        {
            File.Delete(StoneCache);
        }

        var sum = 0;

        var input = Input.Parse(rawInput);
        using (var writer = new StreamWriter(File.Create(StoneCache)))
        {
            foreach (var stone in input.Stones)
            {
                writer.WriteLine(stone);
            }
        }

        var sw = Stopwatch.StartNew();
        for (var i = 0; i < 75; i++)
        {
            Debug.Write($"[{i}]");
            CachedBlink();
            Debug.WriteLine($" took:{sw.ElapsedMilliseconds/1000}s");
        }

        using var reader = new StreamReader(File.OpenRead(StoneCache));
        while (!reader.EndOfStream)
        {
            _ = reader.ReadLine();
            sum++;
        }

        return sum;
    }

    private void CachedBlink()
    {
        using var writer = new StreamWriter(File.Create(UpdatedCache));
        using var reader = new StreamReader(File.OpenRead(StoneCache));

        while (!reader.EndOfStream)
        {
            var stone = long.Parse(reader.ReadLine() ?? throw new InvalidOperationException());
            var updated = UpdateStone(stone);

            foreach (var updatedStone in updated)
            {
                writer.WriteLine(updatedStone);
            }
        }

        File.Delete(StoneCache);
        File.Move(UpdatedCache, StoneCache);
    }

    private IReadOnlyList<long> Blink(IEnumerable<long> stones)
    {
        List<long> updated = [];

        foreach (var stone in stones)
        {
            updated.AddRange(UpdateStone(stone));
        }

        return updated;
    }

    private static List<long> UpdateStone(long stone)
    {
        if (stone == 0)
        {
            return [1];
        }

        var chars = stone.ToString().AsSpan();

        if (chars.Length % 2 == 0)
        {
            var midpoint = chars.Length / 2;
            var left = chars[..midpoint];
            var right = chars[midpoint..];

            return [long.Parse(left), long.Parse(right)];
        }

        return [stone * 2024];
    }
}