using System.Data;
using System.Diagnostics;
using System.Text;
using AOC24.Day2;
using Xunit.Abstractions;

namespace AOC24.Day6;

public class Puzzle6 : IPuzzle<int>
{
    public class Input(char[,] map)
    {
        public char[,] Map { get; } = map;
        public int XBoundery { get; } = map.GetLength(0);
        public int YBoundery { get; } = map.GetLength(1);

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

    private readonly struct Position(int x, int y)
    {
        public readonly int X = x;
        public readonly int Y = y;

        public override string ToString() => $"{X} {Y}";
    }

    public int Day { get; } = 6;

    private readonly ITestOutputHelper _output;

    public Puzzle6(ITestOutputHelper output)
    {
        _output = output;
    }

    public int SolvePartOne(string rawInput)
    {
        int sum = 0;
        var input = Input.Parse(rawInput);

        var direction = Direction.North;
        Position position = new(0, 0);

        // find initial position of guard
        for (int y = 0; y < input.YBoundery; y++)
        {
            for (int x = 0; x < input.XBoundery; x++)
            {
                var ch = input.Map[x, y];

                if (ch == '^')
                {
                    position = new(x, y);
                    sum++;
                    break;
                }
            }
        }

        var bounds = new Position(input.XBoundery, input.YBoundery);

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
                    input.Map[nextCell.X, nextCell.Y] = 'X';
                    position = nextCell;
                    break;
            }

            input.Map[position.X, position.Y] = GetGuardChar(direction);
        }

        RenderMap(input);

        return sum;
    }

    private void RenderMap(Input input)
    {
        var builder = new StringBuilder();

        for (int y = 0; y < input.YBoundery; y++)
        {
            for (int x = 0; x < input.XBoundery; x++)
            {
                builder.Append(input.Map[x, y]);
            }

            builder.AppendLine();
        }

        builder.AppendLine();
        _output.WriteLine(builder.ToString());
    }

    public int SolvePartTwo(string rawInput)
    {
        throw new NotImplementedException();
    }

    private static bool IsInBoundary(Position position, Position maxBounds) => position.X >= 0 && position.Y >= 0 && position.X < maxBounds.X && position.Y < maxBounds.Y;

    private static Position GetNextCell(Position position, Direction direction) => direction switch
    {
        Direction.North => new Position(position.X, position.Y - 1),
        Direction.East => new Position(position.X + 1, position.Y),
        Direction.South => new Position(position.X, position.Y + 1),
        Direction.West => new Position(position.X - 1, position.Y),

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