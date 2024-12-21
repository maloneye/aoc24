using System.Text;
using Xunit.Abstractions;

namespace AOC24.Day6;

public class Puzzle6 : IPuzzle<int>
{
    public class Input(char[,] map)
    {
        public char[,] Map { get; } = map;
        public int XBoundary { get; } = map.GetLength(0);
        public int YBoundary { get; } = map.GetLength(1);

        public static Input Parse(string rawInput)
        {
            var lines = rawInput.Split("\n");
            var lineLength = lines[0].Length;

            var map = new char[lineLength, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lineLength; x++)
                {
                    map[x, y] = lines[y][x];
                }
            }

            return new Input(map);
        }
    }

    private enum Direction
    {
        North,
        East,
        South,
        West,
    }



    public int Day { get; } = 6;
    public bool IsRenderingEnabled { get; set; }
    
    private readonly ITestOutputHelper _output;

    public Puzzle6(ITestOutputHelper output)
    {
        _output = output;
    }

    public int SolvePartOne(string rawInput)
    {
        var input = Input.Parse(rawInput);
        var bounds = new Position<int>(input.XBoundary, input.YBoundary);

        var direction = Direction.North;
        var position = GetInitialPosition(input.Map, bounds);

        int sum = 1;

        while (IsInBoundary(position, bounds))
        {
            input.Map[position.X, position.Y] = 'X';
            var nextCell = GetNextCell(position, direction);

            if (!IsInBoundary(nextCell, bounds))
            {
                break;
            }

            switch (input.Map[nextCell.X, nextCell.Y])
            {
                case '#':
                    direction = Rotate90(direction);
                    break;
                case '.':
                    sum++;
                    goto default;
                default:
                    position = nextCell;
                    break;
            }

            input.Map[position.X, position.Y] = GetGuardChar(direction);
        }

        RenderMap(input.Map, bounds);

        return sum;
    }

    public int SolvePartTwo(string rawInput)
    {
        var sum = 0;
        var input = Input.Parse(rawInput);
        var bounds = new Position<int>(input.XBoundary, input.YBoundary);

        for (int y = 0; y < input.YBoundary; y++)
        {
            for (int x = 0; x < input.XBoundary; x++)
            {
                if (input.Map[x, y] != '.')
                {
                    continue;
                }

                // Adding obstacle
                
                var map = (char[,])input.Map.Clone();
                map[x, y] = 'O';

                if (IsLoopingMap(map, bounds))
                {
                    sum++;
                }
            }
        }

        return sum;
    }

    private bool IsLoopingMap(char[,] map, Position<int> bounds)
    {
        var direction = Direction.North;
        var start = GetInitialPosition(map, bounds);
        var position = start;


        var visited = new Dictionary<Position<int>, List<Direction>>();

        while (IsInBoundary(position, bounds))
        {
            var nextCell = GetNextCell(position, direction);

            if (!IsInBoundary(nextCell, bounds))
            {
                break;
            }

            char cellChar;

            switch (map[nextCell.X, nextCell.Y])
            {
                case 'O':
                    goto case '#';
                case '#':
                    if (visited.TryGetValue(position, out var directions))
                    {
                        if (directions.Contains(direction))
                        {
                            RenderMap(map, bounds);
                            return true;
                        }

                        directions.Add(direction);
                    }
                    else
                    {
                        visited.Add(position, [direction]);
                    }

                    direction = Rotate90(direction);
                    cellChar = '+';
                    break;
                case '.':
                    position = nextCell;
                    cellChar = (position == start) ? '^' : GetCellVisitedChar(direction);
                    break;
                default:
                    position = nextCell;
                    cellChar = (position == start) ? '^' : '+';
                    break;
            }

            map[position.X, position.Y] = cellChar;
        }

        return false;
    }

    private static char GetCellVisitedChar(Direction direction) => direction switch
    {
        Direction.North => '|',
        Direction.South => '|',
        Direction.East => '-',
        Direction.West => '-',
        _ => throw new ArgumentException($"Invalid direction: {direction}")
    };

    private static Position<int> GetInitialPosition(char[,] map, Position<int> bounds)
    {
        for (int y = 0; y < bounds.Y; y++)
        {
            for (int x = 0; x < bounds.X; x++)
            {
                var ch = map[x, y];

                if (ch == '^')
                {
                    return new(x, y);
                }
            }
        }

        throw new ArgumentException("Could not find guard in map");
    }

    private void RenderMap(char[,] map, Position<int> bounds)
    {
        if (!IsRenderingEnabled)
        {
            return;
        }

        var builder = new StringBuilder();

        for (int y = 0; y < bounds.Y; y++)
        {
            for (int x = 0; x < bounds.X; x++)
            {
                builder.Append(map[x, y]);
            }

            builder.AppendLine();
        }

        builder.AppendLine();
        _output.WriteLine(builder.ToString());
    }

    private static bool IsInBoundary(Position<int> position, Position<int> maxBounds) => position.X >= 0 && position.Y >= 0 && position.X < maxBounds.X && position.Y < maxBounds.Y;

    private static Position<int> GetNextCell(Position<int> position, Direction direction) => direction switch
    {
        Direction.North => new Position<int>(position.X, position.Y - 1),
        Direction.East => new Position<int>(position.X + 1, position.Y),
        Direction.South => new Position<int>(position.X, position.Y + 1),
        Direction.West => new Position<int>(position.X - 1, position.Y),

        _ => throw new ArgumentException($"Invalid direction: {direction}"),
    };

    private static char GetGuardChar(Direction direction) => direction switch
    {
        Direction.North => '^',
        Direction.East => '>',
        Direction.South => 'v',
        Direction.West => '<',

        _ => throw new ArgumentException($"Invalid direction: {direction}"),
    };

    private static Direction Rotate90(Direction direction) => ((int)direction + 1) > 3 ? Direction.North : direction + 1;
}