using System.Text;
using AOC24.DayTwo;

namespace AOC24.Day4;

public class Puzzle4 : IPuzzle<int>
{
    public class Input(char[,] crossword)
    {
        public char[,] Crossword { get; } = crossword;
        public int XBoundery { get; } = crossword.GetLength(0);
        public int YBoundery { get; } = crossword.GetLength(1);

        public static Input Parse(string rawInput)
        {
            var lines = rawInput.Split("\n");

            var yBoundery = lines.Length;
            var xBoundery = lines[0].Length;

            var crossword = new char[xBoundery, xBoundery];

            for (int y = 0; y < yBoundery; y++)
            {
                var line = lines[y];

                for (int x = 0; x < xBoundery; x++)
                {
                    crossword[x, y] = line[x];
                }
            }

            return new Input(crossword);
        }
    }

    public int Day { get; } = 4;

    private const string Xmas = "XMAS";

    public int SolvePartOne(string rawInput)
    {
        var sum = 0;
        var input = Input.Parse(rawInput);

        for (int y = 0; y < input.YBoundery; y++)
        {
            for (int x = 0; x < input.XBoundery; x++)
            {
                var ch = input.Crossword[x, y];

                if (ch != 'X')
                {
                    continue;
                }

                //bounds check
                var xPlus = IsInBoundPositive(x, input.XBoundery);
                var xMinus = IsInBoundNegative(x);
                var yPlus = IsInBoundPositive(y, input.YBoundery);
                var yMinus = IsInBoundNegative(y);

                // 0°
                if (yMinus)
                {
                    var section = new StringBuilder();

                    for (int j = y; j > y - 4; j--)
                    {
                        section.Append(input.Crossword[x, j]);
                    }

                    var result = section.ToString();

                    if (result == "XMAS")
                    {
                        sum++;
                    }
                }

                // 45°
                if (xPlus && yMinus)
                {
                    var section = new StringBuilder();

                    for (int index = 0; index < 4; index++)
                    {
                        section.Append(input.Crossword[x + index, y - index]);
                    }

                    var result = section.ToString();

                    if (result == "XMAS")
                    {
                        sum++;
                    }
                }

                // 90°
                if (xPlus)
                {
                    var section = new StringBuilder();

                    for (int i = x; i - x < 4; i++)
                    {
                        section.Append(input.Crossword[i, y]);
                    }

                    var result = section.ToString();

                    if (result == "XMAS")
                    {
                        sum++;
                    }
                }

                // 135°
                if (xPlus && yPlus)
                {
                    var section = new StringBuilder();

                    for (int index = 0; index < 4; index++)
                    {
                        section.Append(input.Crossword[x + index, y + index]);
                    }

                    var result = section.ToString();

                    if (result == "XMAS")
                    {
                        sum++;
                    }
                }

                // 180°
                if (yPlus)
                {
                    var section = new StringBuilder();

                    for (int j = y; j - y < 4; j++)
                    {
                        section.Append(input.Crossword[x, j]);
                    }

                    var result = section.ToString();

                    if (result == "XMAS")
                    {
                        sum++;
                    }
                }

                // 225°
                if (xMinus && yPlus)
                {
                    var section = new StringBuilder();

                    for (int index = 0; index < 4; index++)
                    {
                        section.Append(input.Crossword[x - index, y + index]);
                    }

                    var result = section.ToString();

                    if (result == "XMAS")
                    {
                        sum++;
                    }
                }

                // 270°
                if (xMinus)
                {
                    var section = new StringBuilder();

                    for (int i = x; i > x - 4; i--)
                    {
                        section.Append(input.Crossword[i, y]);
                    }

                    var result = section.ToString();

                    if (result == "XMAS")
                    {
                        sum++;
                    }
                }

                // 315°
                if (xMinus && yMinus)
                {
                    var section = new StringBuilder();

                    for (int index = 0; index < 4; index++)
                    {
                        section.Append(input.Crossword[x - index, y - index]);
                    }

                    var result = section.ToString();

                    if (result == "XMAS")
                    {
                        sum++;
                    }
                }
            }
        }

        return sum;
    }

    private bool IsInBoundPositive(int n, int bound) => n + 3 < bound;

    private bool IsInBoundNegative(int n) => n - 3 >= 0;

    public int SolvePartTwo(string rawInput)
    {
        throw new NotImplementedException();
    }
}