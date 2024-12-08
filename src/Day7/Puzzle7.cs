using System.Collections;
using System.Diagnostics;
using System.Text;
using Xunit.Abstractions;

namespace AOC24.Day7;

public class Puzzle7 : IPuzzle<long>
{
    public class Input(IReadOnlyList<(long Result, List<int> Numbers)> equations)
    {
        public IReadOnlyList<(long Result, List<int> Numbers)> Equations { get; } = equations;

        public static Input Parse(string rawInput)
        {
            List<(long Result, List<int> Numbers)> equations = [];
            var lines = rawInput.Split("\n");

            foreach (var line in lines)
            {
                var parts = line.Split(':');
                var result = long.Parse(parts[0]);

                List<int> numbers = [];

                foreach (var number in parts[1].Split(' ')[1..])
                {
                    numbers.Add(int.Parse(number));
                }

                equations.Add((result, numbers));
            }

            return new Input(equations);
        }
    }

    private enum Operation
    {
        Add,
        Multiply,
    }

    public int Day { get; } = 7;

    public long SolvePartOne(string rawInput)
    {
        var input = Input.Parse(rawInput);
        long sum = 0;

        foreach (var equation in input.Equations)
        {
            var n = equation.Numbers.Count - 1;
            var combinations = Math.Pow(2, equation.Numbers.Count);
            
            for (int i = 0; i < combinations; i++)
            {
                Debug.WriteLine("");

                long result = equation.Numbers[0];
                var map = new BitArray([i]);
                
                for (int j = 0; j < n; j++)
                {
                    var operation = (Operation)(map[j] ? 1 : 0);
                    result = ApplyOperator(result, equation.Numbers[j + 1], operation);
                }

                if (result == equation.Result)
                {
                    sum += result;
                    break;
                }
            }
        }

        return sum;
    }

    public long SolvePartTwo(string rawInput)
    {
        var input = Input.Parse(rawInput);
        var sum = 0;
        return sum;
    }

    private long ApplyOperator(long first, int second, Operation operation) => (operation) switch
    {
        Operation.Add => first + second,
        Operation.Multiply => first * second,
        _ => throw new ArgumentException(nameof(operation)),
    };
}