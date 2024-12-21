using System.Text;

namespace AOC24;

public class CharMapInput(char[,] map)
{
    public char[,] Map { get; } = map;
    public int XBoundary { get; } = map.GetLength(0);
    public int YBoundary { get; } = map.GetLength(1);

    public static CharMapInput Parse(string rawInput)
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

        return new CharMapInput(map);
    }

    public bool IsInBoundary(Position<int> position) => position.X >= 0 && position.Y >= 0 && position.X < XBoundary && position.Y < YBoundary;

    public char[,] CloneMap() => (char[,])Map.Clone();

    public char At(Position<int> position) => Map[position.X, position.Y];

    public void Set(Position<int>  position, char value) => Map[position.X, position.Y] = value;

    public string RenderMap()
    {
        var builder = new StringBuilder();

        for (int y = 0; y < YBoundary; y++)
        {
            for (int x = 0; x < XBoundary; x++)
            {
                builder.Append(map[x, y]);
            }

            builder.AppendLine();
        }

        builder.AppendLine();

        return builder.ToString();
    }
}