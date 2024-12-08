using System.Data;
using System.Text;
using AOC24.Day2;

namespace AOC24.Day6;

public class Puzzle6 : IPuzzle<int>
{
    public class Input(char[,] map)
    {
        public char[,] Map { get; } = map;

        public static Input Parse(string rawInput)
        {
            var lines = rawInput.Split("\n");
            var lineLength = lines[0].Length;
            
            var map = new char[lineLength, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lineLength; x++)
                {
                    map[x, y] = lines[y][x];
                }    
            }

            return new Input(map);
        }
    }

    public int Day { get; } = 6;

    public int SolvePartOne(string rawInput)
    {
        throw new NotImplementedException();
    }

    public int SolvePartTwo(string rawInput)
    {
        throw new NotImplementedException();
    }
}