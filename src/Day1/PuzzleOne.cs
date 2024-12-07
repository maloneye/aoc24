using AOC24.DayTwo;

namespace AOC24.DayOne;

public class PuzzleOne : IPuzzle<int>
{
    public class Input(IEnumerable<int> left, IEnumerable<int> right)
    {
        public IReadOnlyList<int> Left { get; } = left.ToList();
        public IReadOnlyList<int> Right { get; } = right.ToList();

        public static Input Parse(string rawInput)
        {
            var lines = rawInput.Split("\n");

            var left = new List<int>();
            var right = new List<int>();

            foreach (var line in lines)
            {
                var splitIndex = line.IndexOf(' ');

                left.Add(int.Parse(line[..splitIndex]));
                right.Add(int.Parse(line[splitIndex..]));
            }

            return new Input(left, right);
        }
    }

    public int Day { get; } = 1;
    
    public int SolvePartOne(string rawInput)
    {
        var input = Input.Parse(rawInput);
        var left = input.Left.Order().ToList();
        var right = input.Right.Order().ToList();

        int sum = 0;

        for (int i = 0; i < input.Left.Count; i++)
        {
            sum += Math.Abs(left[i] - right[i]);
        }

        return sum;
    }

    public int SolvePartTwo(string rawInput)
    {
        var input = Input.Parse(rawInput);

        var occurrences = new Dictionary<int, int>();

        foreach (var number in input.Right)
        {
            if (!occurrences.TryAdd(number, number))
            {
                occurrences[number]+=number;
            }
        }

        int sum = 0;
        foreach (var number in input.Left)
        {
            if (occurrences.TryGetValue(number, out var occurrence))
            {
                sum += occurrence;
            }
        }

        return sum;
    }
}