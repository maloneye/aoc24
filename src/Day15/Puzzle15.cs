using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AOC24.Day14;
using Xunit.Abstractions;

namespace AOC24.Day15;

public class Puzzle15 : IPuzzle<int>
{
    public class Input : CharMapInput
    {
        public IReadOnlyList<Direction> Directions { get; }

        public Input(IEnumerable<Direction> directions, char[,] map) : base(map)
        {
            Directions = directions.ToList();
        }

        public static Input Parse(string rawInput)
        {
            var span = rawInput.AsSpan();
            var splitPoint = span.IndexOf("\n\n");

            List<Direction> directions = [];
            char[] validChars = ['^', 'v', '<', '>'];

            foreach (var ch in span[splitPoint..])
            {
                if (validChars.Contains(ch))
                {
                    directions.Add((Direction)ch);
                }
            }

            var map = CharMapInput.Parse(span[..splitPoint].ToString());

            return new Input(directions, map.Map);
        }
    }

    public enum Direction
    {
        Up = '^',
        Down = 'v',
        Left = '<',
        Right = '>'
    }

    private class Robot(Position<int> initialPosition)
    {
        public Position<int> Position { get; private set; } = initialPosition;

        public Position<int> PeekNextPosition(Direction direction) => GetNextPosition(direction, Position);

        public void Move(Direction direction) => Position = PeekNextPosition(direction);
    }

    public int Day { get; } = 15;

    private readonly ITestOutputHelper _output;

    public Puzzle15(ITestOutputHelper output)
    {
        _output = output;
    }

    public int SolvePartOne(string rawInput)
    {
        var sum = 0;
        var input = Input.Parse(rawInput);
        // find inital position of robot
        var robot = FindRobot(input);

        // then iterate through the movements
        foreach (var direction in input.Directions)
        {
            var nextPosition = robot.PeekNextPosition(direction);
            var ch = input.At(nextPosition);

            if (ch == '.')
            {
                input.Map[robot.Position.X, robot.Position.Y] = '.';
                input.Map[nextPosition.X, nextPosition.Y] = '@';
                robot.Move(direction);
            }
            else if (ch == 'O'
                     && CanPush(input, nextPosition, direction, out var endPosition))
            {
                input.Map[robot.Position.X, robot.Position.Y] = '.';
                input.Map[nextPosition.X, nextPosition.Y] = '@';
                input.Map[endPosition.X, endPosition.Y] = 'O';
                robot.Move(direction);
            }
        }

        // calculate gps total
        for (var y = 0; y < input.YBoundary; y++)
        {
            for (int x = 0; x < input.XBoundary; x++)
            {
                if (input.Map[x, y] == 'O')
                {
                    sum += 100 * y + x;
                }
            }
        }

        var map =input.RenderMap();
        
        _output.WriteLine(map);
        return sum;
    }

    private static Position<int> GetNextPosition(Direction direction, Position<int> Position) => direction switch
    {
        Direction.Up => new Position<int>(Position.X, Position.Y - 1),
        Direction.Down => new Position<int>(Position.X, Position.Y + 1),
        Direction.Left => new Position<int>(Position.X - 1, Position.Y),
        Direction.Right => new Position<int>(Position.X + 1, Position.Y),
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
    };


    private bool CanPush(Input input, Position<int> nextPosition, Direction direction, out Position<int> endPosition)
    {
        endPosition = new Position<int>(-1, -1);
        var ch = 'O';
        do
        {
            nextPosition = GetNextPosition(direction, nextPosition);
            ch = input.At(nextPosition);
        } while (ch == 'O');

        if (ch == '.')
        {
            endPosition = nextPosition;
            return true;
        }

        return false;
    }

    private static Robot FindRobot(Input input)
    {
        for (var y = 0; y < input.YBoundary; y++)
        {
            for (var x = 0; x < input.XBoundary; x++)
            {
                if (input.Map[x, y] == '@')
                {
                    return new(new(x, y));
                }
            }
        }

        throw new InvalidDataException("Could not find robot position.");
    }

    public int SolvePartTwo(string rawInput)
    {
        var input = Input.Parse(rawInput);

        return 0;
    }
}