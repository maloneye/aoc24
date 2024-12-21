using System.Numerics;

namespace AOC24;

public readonly struct Position<T>(T x, T y) where T: INumber<T> 
{
    public readonly T X = x;
    public readonly T Y = y;

    public override string ToString() => $"{X} {Y}";

    public override bool Equals(object? obj) =>
        obj is Position<T> other && X == other.X && Y == other.Y;

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public static bool operator ==(Position<T> left, Position<T> right) => left.Equals(right);

    public static bool operator !=(Position<T> left, Position<T> right) => !(left == right);
    
    public static Position<T> operator +(Position<T> left, Position<T> right) => new Position<T>(left.X + right.X, left.Y + right.Y);
}
