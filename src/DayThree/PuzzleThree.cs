using System.Text;

namespace AOC24.DayTwo;

public class PuzzleThree : IPuzzle<int>
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

    private const string OpeningTag = "mul(";

    public int SolvePartOne(string rawInput)
    {
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
        throw new NotImplementedException();
    }

    private static int GetTotalForLine(string line)
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

    private static bool IsOpeningTag(char ch, string line, ref int i)
    {
        if (ch == 'm' && line.Length >= i + 7)
        {
            var end = i + 4;

            var isOpeningTag = line[i..end] is OpeningTag;
            i += 3;

            return isOpeningTag;
        }

        return false;
    }
}