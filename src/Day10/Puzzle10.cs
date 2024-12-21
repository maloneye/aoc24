using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Transactions;
using Xunit.Abstractions;

namespace AOC24.Day10;

public class Puzzle10 : IPuzzle<long>
{
    public int Day { get; } = 10;
    public bool IsRenderingEnabled { get; set; }

    private readonly ITestOutputHelper _output;

    public Puzzle10(ITestOutputHelper output)
    {
        _output = output;
    }

    public long SolvePartOne(string rawInput)
    {
        var input = MapInput.Parse(rawInput);
        var sum = 0;

        for (int y = 0; y < input.YBoundary; y++)
        {
            for (int x = 0; x < input.XBoundary; x++)
            {
                var element = input.Map[x, y];
                if (element == 0)
                {
                    sum += FindTrails(input, new Position<int>(x, y), 0,[]);
                }
            }
        }

        return sum;
    }

    public long SolvePartTwo(string rawInput)
    {
        var input = MapInput.Parse(rawInput);
        var sum = 0;

        for (int y = 0; y < input.YBoundary; y++)
        {
            for (int x = 0; x < input.XBoundary; x++)
            {
                var element = input.Map[x, y];
                if (element == 0)
                {
                    sum += FindTrailsDistinct(input, new Position<int>(x, y), 0);
                }
            }
        }
        
        return sum;
    }

    private int FindTrails(MapInput input, Position<int> position, int height, List<Position<int>> trailTops)
    {
        if (height == 9)
        {
            if (trailTops.Contains(position))
            {
                return 0;
            }
            
            trailTops.Add(position);
            return 1;
        }

        int sum = 0;
        height++;

        // check north
        var north = new Position<int>(position.X, position.Y - 1);
        if (input.IsInBoundary(north)
            && IsValidHeight(input, height, north))
        {
            sum += FindTrails(input, north, height, trailTops);
        }

        // check east
        var east = new Position<int>(position.X + 1, position.Y);
        if (input.IsInBoundary(east)
            && IsValidHeight(input, height, east))
        {
            sum += FindTrails(input, east, height, trailTops);
        }

        // check south
        var south = new Position<int>(position.X, position.Y + 1);
        if (input.IsInBoundary(south)
            && IsValidHeight(input, height, south))
        {
            sum += FindTrails(input, south, height, trailTops);
        }

        // check west
        var west = new Position<int>(position.X - 1, position.Y);
        if (input.IsInBoundary(west)
            && IsValidHeight(input, height, west))
        {
            sum += FindTrails(input, west, height, trailTops);
        }

        return sum;
    }

    private int FindTrailsDistinct(MapInput input, Position<int> position, int height)
    {
        if (height == 9)
        {
            return 1;
        }

        int sum = 0;
        height++;

        // check north
        var north = new Position<int>(position.X, position.Y - 1);
        if (input.IsInBoundary(north)
            && IsValidHeight(input, height, north))
        {
            sum += FindTrailsDistinct(input, north, height);
        }

        // check east
        var east = new Position<int>(position.X + 1, position.Y);
        if (input.IsInBoundary(east)
            && IsValidHeight(input, height, east))
        {
            sum += FindTrailsDistinct(input, east, height);
        }

        // check south
        var south = new Position<int>(position.X, position.Y + 1);
        if (input.IsInBoundary(south)
            && IsValidHeight(input, height, south))
        {
            sum += FindTrailsDistinct(input, south, height);
        }

        // check west
        var west = new Position<int>(position.X - 1, position.Y);
        if (input.IsInBoundary(west)
            && IsValidHeight(input, height, west))
        {
            sum += FindTrailsDistinct(input, west, height);
        }

        return sum;
    }
    
    private static bool IsValidHeight(MapInput input, int height, Position<int> position) =>
        input.Map[position.X, position.Y] == height;
}