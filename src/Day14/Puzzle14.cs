using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xunit.Abstractions;

namespace AOC24.Day14;

public class Puzzle14 : IPuzzle<int>
{
    public class Input(IEnumerable<Robot> robots)
    {
        public IReadOnlyList<Robot> Robots { get; } = robots.ToList();

        public static Input Parse(string rawInput)
        {
            Position<int> ExtractPosition(ReadOnlySpan<char> span)
            {
                var start = span.IndexOf('=') + 1;
                var mid = span.IndexOf(',');
                var x = int.Parse(span[start..mid]);
                var y = int.Parse(span[(mid + 1)..]);

                return new Position<int>(x, y);
            }

            var lines = rawInput.Split('\n');
            List<Robot> robots = [];
            foreach (var line in lines)
            {
                var span = line.AsSpan();

                var delimiter = span.IndexOf(' ');

                var position = ExtractPosition(span[..delimiter]);
                var velocity = ExtractPosition(span[delimiter..]);

                var robot = new Robot(position, velocity);
                robots.Add(robot);
            }

            return new Input(robots);
        }
    }

    public class Robot(Position<int> position, Position<int> velocity)
    {
        public Position<int> Velocity { get; } = velocity;
        public Position<int> Position { get; private set; } = position;

        public void Move(Position<int> lowerBounds, Position<int> upperBounds)
        {
            var x = Velocity.X + Position.X;
            var y = Velocity.Y + Position.Y;

            x = x < lowerBounds.X ? x + upperBounds.X : x;
            y = y < lowerBounds.Y ? y + upperBounds.Y : y;

            x = x >= upperBounds.X ? x - upperBounds.X : x;
            y = y >= upperBounds.Y ? y - upperBounds.Y : y;

            Position = new Position<int>(x, y);
        }
    }

    public int Day { get; } = 14;

    public Position<int> LowerBounds { get; set; } = new Position<int>(0, 0);
    public Position<int> UpperBounds { get; private set; } = new Position<int>(0, 0);

    public Puzzle14()
    {
    }

    public void SetBounds(int x, int y) => UpperBounds = new Position<int>(x, y);

    public int SolvePartOne(string rawInput)
    {
        var sum = 0;
        var input = Input.Parse(rawInput);

        for (int i = 0; i < 100; i++)
        {
            foreach (var robot in input.Robots)
            {
                robot.Move(LowerBounds, UpperBounds);
            }
        }

        //quadrants
        var first = input.Robots
            .Where(robot => robot.Position.X < UpperBounds.X / 2 && robot.Position.Y < UpperBounds.Y / 2)
            ?.Count() ?? 0;

        var second = input.Robots
            .Where(robot => robot.Position.X > UpperBounds.X / 2 && robot.Position.Y < UpperBounds.Y / 2)
            ?.Count() ?? 0;
        var third = input.Robots
            .Where(robot => robot.Position.X > UpperBounds.X / 2 && robot.Position.Y > UpperBounds.Y / 2)
            ?.Count() ?? 0;
        var fourth = input.Robots
            .Where(robot => robot.Position.X < UpperBounds.X / 2 && robot.Position.Y > UpperBounds.Y / 2)
            ?.Count() ?? 0;

        sum = first * second * third * fourth;

        return sum;
    }

    public int SolvePartTwo(string rawInput)
    {
        var input = Input.Parse(rawInput);

        var i = 0;
        var hits = 0;

        while (hits < 11)
        {
            i++;
            foreach (var robot in input.Robots)
            {
                robot.Move(LowerBounds, UpperBounds);
            }
            
            hits = input.Robots.Where(r => r.Position.X > UpperBounds.X / 2 - 2
                                           && r.Position.Y > UpperBounds.Y / 2 - 2
                                           && r.Position.X <= UpperBounds.X / 2 + 2
                                           && r.Position.Y <= UpperBounds.Y / 2 + 2)?.Count() ?? 0;
        }

        return i;
    }

    public async Task SolvePartTwoAsync(string rawInput)
    {
        Console.CursorVisible = false;
        var winner = 0;
        var input = Input.Parse(rawInput);
        for (int i = 0; i < 100000; i++)
        {
            foreach (var robot in input.Robots)
            {
                robot.Move(LowerBounds, UpperBounds);
            }

            //var window = input.Robots;
            var window = input.Robots.Where(r => r.Position.X > UpperBounds.X / 2 - 2
                                                 && r.Position.Y > UpperBounds.Y / 2 - 2
                                                 && r.Position.X <= UpperBounds.X / 2 + 2
                                                 && r.Position.Y <= UpperBounds.Y / 2 + 2);
            var hits = window.Count();

            winner = Math.Max(winner, hits);

            Console.Clear();
            if (hits == 11)
            {
                await Render(input, i);
            }
            else
            {
                await Console.Out.WriteLineAsync($"nothing at: {i}");
                await Console.Out.WriteLineAsync($"hits: {hits}");
                await Console.Out.WriteLineAsync($"highest: {winner}");
            }
        }
    }

    private async Task Render(Input input, int iteration)
    {
        var builder = new StringBuilder();
        for (int y = 0; y < UpperBounds.Y; y++)
        {
            for (int x = 0; x < UpperBounds.X; x++)
            {
                var character = input.Robots.Any(robot => robot.Position.X == x && robot.Position.Y == y)
                    ? '*'
                    : ' ';
                builder.Append(character);
            }

            builder.AppendLine();
        }

        await Console.Out.WriteLineAsync(builder.ToString());
        await Console.Out.WriteLineAsync($"{iteration}");
        await Console.Out.WriteLineAsync(new string('-', UpperBounds.X));
        var key = Console.ReadKey();
        if (key.Key == ConsoleKey.Escape)
        {
            Environment.Exit(0);
        }
    }
}