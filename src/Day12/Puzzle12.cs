using Xunit.Abstractions;

namespace AOC24.Day12;

public class Puzzle12 : IPuzzle<long>
{
    private class Region(char descriminator)
    {
        public List<Position> Positions { get; } = [];
        public char Descriminator { get; } = descriminator;

        public void Add(Position position)
        {
            if (Positions.Contains(position))
            {
                return;
            }

            Positions.Add(position);
        }

        public int CalculateArea() => Positions.Count;

        public int CalculatePerimeter()
        {
            var perimeter = 0;
            foreach (Position position in Positions)
            {
                perimeter += 4 - CountNeighbours(position, Positions);
            }

            return perimeter;
        }

        public int CalculateSides()
        {
            return 0;
        }

        private int CountNeighbours(Position position, IReadOnlyList<Position> positions)
        {
            int neighbours = 0;

            var north = new Position(position.X, position.Y - 1);
            var east = new Position(position.X + 1, position.Y);
            var south = new Position(position.X, position.Y + 1);
            var west = new Position(position.X - 1, position.Y);

            neighbours += positions.Contains(north) ? 1 : 0;
            neighbours += positions.Contains(east) ? 1 : 0;
            neighbours += positions.Contains(west) ? 1 : 0;
            neighbours += positions.Contains(south) ? 1 : 0;

            return neighbours;
        }
    }

    public int Day { get; } = 12;

    private readonly ITestOutputHelper _output;

    public Puzzle12(ITestOutputHelper output)
    {
        _output = output;
    }

    private List<Position> _seen = [];

    public long SolvePartOne(string rawInput)
    {
        var sum = 0;
        var input = CharMapInput.Parse(rawInput);
        List<Region> regions = [];
        _seen = [];
        for (var y = 0; y < input.YBoundary; y++)
        {
            for (var x = 0; x < input.XBoundary; x++)
            {
                var position = new Position(x, y);
                if (_seen.Contains(position))
                {
                    continue;
                }

                var discriminator = input.At(position);
                var region = new Region(discriminator);
                DiscoverRegion(position, input, region);
                regions.Add(region);
            }
        }

        foreach (var region in regions)
        {
            var area = region.CalculateArea();
            var perimeter = region.CalculatePerimeter();

            sum += area * perimeter;
        }

        return sum;
    }

    private void DiscoverRegion(Position position, CharMapInput input, Region region)
    {
        // check north
        var north = new Position(position.X, position.Y - 1);
        AddIfValid(north, input, region);

        // check east
        var east = new Position(position.X + 1, position.Y);
        AddIfValid(east, input, region);

        // check south
        var south = new Position(position.X, position.Y + 1);
        AddIfValid(south, input, region);

        // check west
        var west = new Position(position.X - 1, position.Y);
        AddIfValid(west, input, region);

        if (region.Positions.Count == 0)
        {
            _seen.Add(position);
            region.Add(position);
        }
    }

    private void AddIfValid(Position position, CharMapInput input, Region region)
    {
        if (input.IsInBoundary(position)
            && input.At(position) == region.Descriminator &&
            !_seen.Contains(position))
        {
            _seen.Add(position);
            region.Add(position);
            DiscoverRegion(position, input, region);
        }
    }

    public long SolvePartTwo(string rawInput)
    {
        return 0;
    }
}