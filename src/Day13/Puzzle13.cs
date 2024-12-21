using System.Security.Cryptography.X509Certificates;
using Xunit.Abstractions;

namespace AOC24.Day13;

public class Puzzle13 : IPuzzle<long>
{
    public class Input(IEnumerable<Run> runs)
    {
        public IReadOnlyList<Run> Runs { get; } = runs.ToList();

        public static Input Parse(string rawInput)
        {
            var lines = rawInput.Split('\n');

            var runs = new List<Run>();
            for (int i = 0; i < lines.Length; i += 4)
            {
                var body = lines.Skip(i).Take(3).ToArray();
                var aStr = body[0].AsSpan();
                var aStart = aStr.IndexOf('+') + 1;
                var aMid = aStr.IndexOf(',');
                var aEnd = aStr.LastIndexOf('+') +1;
                var aX = int.Parse(aStr[aStart..aMid]);
                var aY = int.Parse(aStr[aEnd..]);
                var a = new Position<long>(aX, aY);

                var bStr = body[1].AsSpan();
                var bStart = bStr.IndexOf('+') + 1;
                var bMid = bStr.IndexOf(',');
                var bEnd = bStr.LastIndexOf('+') + 1;
                var bX = int.Parse(bStr[bStart..bMid]);
                var bY = int.Parse(bStr[bEnd..]);
                var b = new Position<long>(bX, bY);

                var prizeStr = body[2].AsSpan();
                var prizeStart = prizeStr.IndexOf('=') + 1;
                var prizeMid = prizeStr.IndexOf(',');
                var prizeEnd = prizeStr.LastIndexOf('=')+1;
                var prizeX = int.Parse(prizeStr[prizeStart..prizeMid]);
                var prizeY = int.Parse(prizeStr[prizeEnd..]);
                var prize = new Position<long>(prizeX, prizeY);

                runs.Add(new(a, b, prize));
            }

            return new Input(runs);
        }
    }

    public class Run(Position<long> a, Position<long> b, Position<long> prize)
    {
        public Position<long> A { get; } = a;
        public Position<long> B { get; } = b;
        public Position<long> Prize { get; private set; } = prize;

        public int Tokens { get; private set; } = 0;

        private const long CorrectionFactor = 10000000000000;
        
        public void AdjustForConversionError() => Prize = new Position<long>(CorrectionFactor*Prize.X, CorrectionFactor*Prize.Y);
        
        public bool Solve()
        {
            // solve simultaneous equations
            var a = (Prize.X*B.Y - Prize.Y*B.X)/(double)(A.X*B.Y -B.X*A.Y);
            var b = (Prize.X - A.X * a) / B.X;

            var tokens = a * 3 + b;
            Tokens = (int)tokens;

            return tokens % 1 == 0 && a< 100 && b<100;
        }
    }

    public int Day { get; } = 13;

    private readonly ITestOutputHelper _output;

    public Puzzle13(ITestOutputHelper output)
    {
        _output = output;
    }

    public long SolvePartOne(string rawInput)
    {
        var sum = 0;
        var input = Input.Parse(rawInput);

        foreach (var run in input.Runs)
        {
            if (run.Solve())
            {
                sum += run.Tokens;
            }
        }
        
        return sum;
    }

    public long SolvePartTwo(string rawInput)
    {
        return 0;
    }
}