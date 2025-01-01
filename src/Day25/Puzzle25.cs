using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AOC24.Day14;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AOC24.Day25;

public class Puzzle25 : IPuzzle<int>
{
    public class Input(IEnumerable<Pins> locks, IEnumerable<Pins> keys)
    {
        public IReadOnlyList<Pins> Locks { get; } = locks.ToList();
        public IReadOnlyList<Pins> Keys { get; } = keys.ToList();

        public static Input Parse(string rawInput)
        {
            var span = rawInput.AsSpan();
            var start = 0;
            var end = -1;

            List<Pins> locks = [];
            List<Pins> keys = [];

            while (start < span.Length)
            {
                var index = span[start..].IndexOf("\n\n");
                end = index == -1 ? span.Length : start + index;

                var slice = span[start..end];

                var pin0 = GetPin(0, slice);
                var pin1 = GetPin(1, slice);
                var pin2 = GetPin(2, slice);
                var pin3 = GetPin(3, slice);
                var pin4 = GetPin(4, slice);

                var pins = new Pins(pin0, pin1, pin2, pin3, pin4);

                if (slice[0] == '#')
                {
                    locks.Add(pins);
                }
                else
                {
                    keys.Add(pins);
                }

                start = end + 2;
            }

            return new Input(locks, keys);
        }

        private static int GetPin(int offset, ReadOnlySpan<char> slice)
        {
            var sum = 0;

            for (int y = offset; y < slice.Length; y += 6)
            {
                if (slice[y] == '#')
                {
                    sum++;
                }
            }

            return sum;
        }
    }

    public readonly struct Pins : IEquatable<Pins>
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

        public override bool Equals(object obj)
        {
            return obj is Pins pins && Equals(pins);
        }

        public bool Equals(Pins other)
        {
            return Pin0 == other.Pin0 &&
                   Pin1 == other.Pin1 &&
                   Pin2 == other.Pin2 &&
                   Pin3 == other.Pin3 &&
                   Pin4 == other.Pin4;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Pin0);
            hash.Add(Pin1);
            hash.Add(Pin2);
            hash.Add(Pin3);
            hash.Add(Pin4);
            return hash.ToHashCode();
        }

        public static bool operator ==(Pins left, Pins right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Pins left, Pins right)
        {
            return !(left == right);
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

        for (int i = 0; i < input.Keys.Count; i++)
        {
            var key = input.Keys[i];

            bool KeyFits(Pins @lock) => key.Pin0 + @lock.Pin0 <= 7 && key.Pin1 + @lock.Pin1 <= 7 && key.Pin2 + @lock.Pin2 <= 7 && key.Pin3 + @lock.Pin3 <= 7 && key.Pin4 + @lock.Pin4 <= 7;

            sum += input.Locks.Count(KeyFits);
        }

        return sum;
    }

    public int SolvePartTwo(string rawInput)
    {
        var input = Input.Parse(rawInput);

        return 0;
    }
}