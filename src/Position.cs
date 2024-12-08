namespace AOC24;

public readonly struct Position(int x, int y)
{
    public readonly int X = x;
    public readonly int Y = y;

    public override string ToString() => $"{X} {Y}";

    public override bool Equals(object? obj) =>
        obj is Position other && X == other.X && Y == other.Y;

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public static bool operator ==(Position left, Position right) => left.Equals(right);

    public static bool operator !=(Position left, Position right) => !(left == right);
}