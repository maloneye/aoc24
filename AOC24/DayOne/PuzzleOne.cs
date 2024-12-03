namespace AOC24.DayOne;

public class PuzzleOne : IPuzzle<int>
{
    public class Input(IEnumerable<int> left, IEnumerable<int> right)
    {
        public IReadOnlyList<int> Left { get; } = left.ToList();
        public IReadOnlyList<int> Right { get; } = right.ToList();

        public static Input Parse(StreamReader reader)
        {
            var left = new List<int>();
            var right = new List<int>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine().AsSpan();
                var splitIndex = line.IndexOf(' ');

                left.Add(int.Parse(line[..splitIndex]));
                right.Add(int.Parse(line[splitIndex..]));
            }

            return new Input(left, right);
        }
    }

    private readonly Input _input;

    public PuzzleOne(Input input)
    {
        _input = input;
    }

    public int SolvePartOne()
    {
        var Left = _input.Left.Order().ToList();
        var Right = _input.Right.Order().ToList();

        int sum = 0;

        for (int i = 0; i < _input.Left.Count; i++)
        {
            sum += Math.Abs(Left[i] - Right[i]);
        }

        return sum;
    }

    public int SolvePartTwo()
    {
        var occurances = new Dictionary<int, int>();

        foreach (var number in _input.Right)
        {
            if (!occurances.TryAdd(number, number))
            {
                occurances[number]+=number;
            }
        }

        int sum = 0;
        foreach (var number in _input.Left)
        {
            if (occurances.TryGetValue(number, out int occurance))
            {
                sum += occurance;
            }
        }

        return sum;
    }
}