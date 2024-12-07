using System.Security.Cryptography;
using System.Text;

namespace AOC24.DayTwo;

public class Puzzle3 : IPuzzle<int>
{
    public class Input(IReadOnlyList<string> instructions)
    {
        public IReadOnlyList<string> Instructions { get; } = instructions;

        public static Input Parse(string rawData)
        {
            var instructions = rawData.Split("\n");

            return new Input(instructions);
        }
    }

    public enum State
    {
        OpeningTag,
        FirstNumber,
        SecondNumber,
    }

    public int Day { get; } = 3;

    private const string MulTag = "mul(";
    private const string DoTag = "do()";
    private const string DontTag = "don't()";
    
    private bool _isMultiplyEnabled;
    private bool _ignoreModifiers;
    public int SolvePartOne(string rawInput)
    {
        _isMultiplyEnabled = true;
        _ignoreModifiers = true;
        var input = Input.Parse(rawInput);

        var total = 0;

        foreach (var line in input.Instructions)
        {
            total += GetTotalForLine(line);
        }

        return total;
    }

    public int SolvePartTwo(string rawInput)
    {
        _isMultiplyEnabled = true;
        _ignoreModifiers = false;
        var input = Input.Parse(rawInput);

        var total = 0;

        foreach (var line in input.Instructions)
        {
            total += GetTotalForLine(line);
        }

        return total;
    }

    private int GetTotalForLine(string line)
    {
        var total = 0;
        var state = State.OpeningTag;
        var firstNumber = new StringBuilder();
        var secondNumber = new StringBuilder();

        for (var i = 0; i < line.Length; i++)
        {
            var ch = line[i];

            switch (state)
            {
                case State.OpeningTag:

                    if (IsOpeningTag(ch, line, ref i))
                    {
                        i += 3;
                        state = State.FirstNumber;
                    }

                    continue;

                case State.FirstNumber:

                    if (char.IsNumber(ch))
                    {
                        firstNumber.Append(ch);
                        continue;
                    }

                    if (ch == ',' && firstNumber.Length > 0)
                    {
                        state = State.SecondNumber;
                        continue;
                    }

                    state = State.OpeningTag;
                    firstNumber = new();
                    continue;

                case State.SecondNumber:

                    if (char.IsNumber(ch))
                    {
                        secondNumber.Append(ch);
                        continue;
                    }

                    if (ch == ')' && secondNumber.Length > 0)
                    {
                        total += int.Parse(firstNumber.ToString()) * int.Parse(secondNumber.ToString());
                    }

                    state = State.OpeningTag;
                    firstNumber = new();
                    secondNumber = new();
                    continue;

                default: throw new ArgumentOutOfRangeException();
            }
        }

        return total;
    }

    private bool IsOpeningTag(char ch, string line,ref int i)
    {
        int end = i + 7;
        if (line.Length < end)
        {
            return false;
        }

        switch (ch)
        {
            case 'm':
            {
                end = i + 4;
                return line[i..end] is MulTag && _isMultiplyEnabled;
            }
            
            case 'd':
                if (_ignoreModifiers)
                {
                    break;
                }

                var slice = line[i..end];

                if (slice.Contains(DoTag))
                {
                    i += 3;
                    _isMultiplyEnabled = true;
                }
                else if (slice.Contains(DontTag))
                {
                    i += 6;
                    _isMultiplyEnabled = false;
                }

                break;
        }
        
        return false;
    }
}