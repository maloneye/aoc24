using System.Text;

namespace AOC24;

public class MapInput(int[,] map)
{
    public int[,] Map { get; } = map;
    public int XBoundary { get; } = map.GetLength(0);
    public int YBoundary { get; } = map.GetLength(1);

    public static MapInput Parse(string rawInput)
    {
        var lines = rawInput.Split("\n");
        var lineLength = lines[0].Length;

        var map = new int[lineLength, lines.Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lineLength; x++)
            {
                map[x, y] = int.Parse(lines[y][x].ToString());
            }
        }

        return new MapInput(map);
    }

    public bool IsInBoundary(Position position) =>
        position.X >= 0 && position.Y >= 0 && position.X < XBoundary && position.Y < YBoundary;

    public int[,] CloneMap() => (int[,])Map.Clone();

    public int At(Position position) => Map[position.X, position.Y];

    public void Set(Position position, char value) => Map[position.X, position.Y] = value;
}