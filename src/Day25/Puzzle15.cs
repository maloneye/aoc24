using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AOC24.Day14;
using Xunit.Abstractions;

namespace AOC24.Day25;

public class Puzzle25 : IPuzzle<int>
{
    public class Input(IEnumerable<Pins> keys, IEnumerable<Pins> locks)
    {
        private IReadOnlyList<Pins> Keys { get; } = keys.ToList();
        private IReadOnlyList<Pins> Locks { get; } = locks.ToList();

        public static Input Parse(string rawInput)
        {
            var span = rawInput.AsSpan();
            var start = 0;
            var end = -1;

            List<Pins> keys = [];
            List<Pins> locks = [];

            while (start < span.Length)
            {
                end = span[start..].IndexOf("\n\n");

                if (end == -1)
                {
                    break;
                }

                var slice = span[start..(start + end)];

                var pin0 = GetPin(0, slice);
                var pin1 = GetPin(0, slice);
                var pin2 = GetPin(0, slice);
                var pin3 = GetPin(0, slice);
                var pin4 = GetPin(0, slice);

                var pins = new Pins(pin0, pin1, pin2, pin3, pin4);

                if (slice[0] == '#')
                {
                    keys.Add(pins);
                }
                else
                {
                    locks.Add(pins);
                }

                start = start + end + 2;
            }

            return new Input(keys, locks);
        }

        private static int GetPin(int offset, ReadOnlySpan<char> slice)
        {
            var sum = 0;

            for (int y = offset; y < slice.Length; y += 5)
            {
                if (slice[y] == '#')
                {
                    sum++;
                }
            }

            return sum;
        }
    }

    public readonly struct Pins
    {
        public int Pin0 { get; }
        public int Pin1 { get; }
        public int Pin2 { get; }
        public int Pin3 { get; }
        public int Pin4 { get; }


        public Pins(int pin0, int pin1, int pin2, int pin3, int pin4)
        {
            Pin0 = pin0;
            Pin1 = pin1;
            Pin2 = pin2;
            Pin3 = pin3;
            Pin4 = pin4;
        }
    }

    public int Day { get; } = 25;

    private readonly ITestOutputHelper _output;

    public Puzzle25(ITestOutputHelper output)
    {
        _output = output;
    }

    public int SolvePartOne(string rawInput)
    {
        var sum = 0;
        var input = Input.Parse(rawInput);


        return sum;
    }

    public int SolvePartTwo(string rawInput)
    {
        var input = Input.Parse(rawInput);

        return 0;
    }
}